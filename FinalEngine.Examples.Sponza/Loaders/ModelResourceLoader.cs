namespace FinalEngine.Examples.Sponza.Loaders;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Numerics;
using Assimp;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Primitives;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;
using Material = Rendering.Geometry.Material;
using Mesh = Rendering.Geometry.Mesh;

public class ModelResource : IResource
{
    public IEnumerable<Model> Models { get; set; }
}

public class ModelResourceLoader : ResourceLoaderBase<ModelResource>
{
    private readonly IFileSystem fileSystem;

    private readonly IRenderDevice renderDevice;

    public ModelResourceLoader(IRenderDevice renderDevice, IFileSystem fileSystem)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public override ModelResource LoadResource(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"The specified {nameof(filePath)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(filePath));
        }

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
        }

        using (var context = new AssimpContext())
        {
            var scene = context.ImportFile(
                filePath,
                PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs | PostProcessSteps.GenerateSmoothNormals | PostProcessSteps.CalculateTangentSpace);

            if (scene == null || scene.SceneFlags.HasFlag(SceneFlags.Incomplete) || scene.RootNode == null)
            {
                throw new AssimpException($"Failed to load model using Assimp from path: '{filePath}'");
            }

            string? directory = Path.GetDirectoryName(filePath);

            return new ModelResource()
            {
                Models = this.ProcessNode(scene, scene.RootNode, directory),
            };
        }
    }

    private IMaterial LoadMaterial(Assimp.Mesh mesh, List<Assimp.Material> materials, string? directory)
    {
        var result = new Material();

        if (mesh.MaterialIndex >= 0)
        {
            var assimpMaterial = materials[mesh.MaterialIndex];

            if (assimpMaterial.HasTextureDiffuse)
            {
                result.DiffuseTexture = ResourceManager.Instance.LoadResource<ITexture2D>($"{directory}\\{assimpMaterial.TextureDiffuse.FilePath}");
            }

            if (assimpMaterial.HasTextureSpecular)
            {
                result.SpecularTexture = ResourceManager.Instance.LoadResource<ITexture2D>($"{directory}\\{assimpMaterial.TextureSpecular.FilePath}");
            }

            if (assimpMaterial.HasTextureHeight)
            {
                result.NormalTexture = ResourceManager.Instance.LoadResource<ITexture2D>($"{directory}\\{assimpMaterial.TextureHeight.FilePath}");
            }

            result.Shininess = assimpMaterial.Shininess * assimpMaterial.ShininessStrength;
        }

        return result;
    }

    private Model ProcessMesh(Scene scene, Assimp.Mesh mesh, string? directory)
    {
        var vertices = new List<MeshVertex>();
        var indices = new List<int>();

        if (mesh.HasVertices)
        {
            for (int i = 0; i < mesh.VertexCount; i++)
            {
                MeshVertex vertex = default;

                vertex.Position = new Vector3(
                    mesh.Vertices[i].X,
                    mesh.Vertices[i].Y,
                    mesh.Vertices[i].Z);

                if (mesh.HasTextureCoords(0))
                {
                    vertex.TextureCoordinate = new Vector2(
                        mesh.TextureCoordinateChannels[0][i].X,
                        mesh.TextureCoordinateChannels[0][i].Y);
                }

                if (mesh.HasNormals)
                {
                    vertex.Normal = new Vector3(
                        mesh.Normals[i].X,
                        mesh.Normals[i].Y,
                        mesh.Normals[i].Z);
                }

                if (mesh.HasTangentBasis)
                {
                    vertex.Tangent = new Vector3(
                        mesh.Tangents[i].X,
                        mesh.Tangents[i].Y,
                        mesh.Tangents[i].Z);
                }

                vertices.Add(vertex);
            }
        }

        for (int i = 0; i < mesh.FaceCount; i++)
        {
            var face = mesh.Faces[i];

            for (int j = 0; j < face.IndexCount; j++)
            {
                indices.Add(face.Indices[j]);
            }
        }

        return new Model()
        {
            Mesh = new Mesh(this.renderDevice.Factory, [.. vertices], [.. indices], false, false),
            Material = this.LoadMaterial(mesh, scene.Materials, directory),
        };
    }

    private IList<Model> ProcessNode(Scene scene, Node node, string? directory)
    {
        var meshes = new List<Model>();

        for (int i = 0; i < node.MeshCount; i++)
        {
            var mesh = scene.Meshes[node.MeshIndices[i]];
            meshes.Add(this.ProcessMesh(scene, mesh, directory));
        }

        for (int i = 0; i < node.ChildCount; i++)
        {
            meshes.AddRange(this.ProcessNode(scene, node.Children[i], directory));
        }

        return meshes;
    }
}
