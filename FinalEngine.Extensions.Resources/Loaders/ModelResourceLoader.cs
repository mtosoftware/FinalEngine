// <copyright file="ModelResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Extensions.Resources.Loaders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Numerics;
    using Assimp;
    using FinalEngine.IO;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;
    using FinalEngine.Resources;
    using Material = Rendering.Material;
    using Mesh = Rendering.Mesh;

    public class Model : IResource
    {
        public Model(IList<ModelData> modelDatas)
        {
            this.ModelDatas = modelDatas ?? throw new ArgumentNullException(nameof(modelDatas));
        }

        public IList<ModelData> ModelDatas { get; }

        public void Dispose()
        {
            while (this.ModelDatas.Count > 0)
            {
                ResourceManager.Instance.UnloadResource(this.ModelDatas[0].Material.DiffuseTexture);
                ResourceManager.Instance.UnloadResource(this.ModelDatas[0].Material.SpecularTexture);

                this.ModelDatas[0].Mesh.Dispose();
                this.ModelDatas.RemoveAt(0);
            }
        }
    }

    public class ModelData
    {
        public IMaterial Material { get; init; }

        public IMesh Mesh { get; init; }
    }

    public class ModelResourceLoader : ResourceLoaderBase<Model>
    {
        private readonly IFileSystem fileSystem;

        private readonly IRenderDevice renderDevice;

        public ModelResourceLoader(IRenderDevice renderDevice, IFileSystem fileSystem)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public override Model LoadResource(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"The specified {nameof(filePath)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(filePath));
            }

            if (!this.fileSystem.FileExists(filePath))
            {
                throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
            }

            using (var context = new AssimpContext())
            {
                var scene = context.ImportFile(filePath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs | PostProcessSteps.GenerateSmoothNormals);

                if (scene == null || scene.SceneFlags.HasFlag(SceneFlags.Incomplete) || scene.RootNode == null)
                {
                    throw new AssimpException($"Failed to load model using Assimp from path: '{filePath}'");
                }

                string? directory = Path.GetDirectoryName(filePath);

                return new Model(this.ProcessNode(scene, scene.RootNode, directory));
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

                result.Shininess = assimpMaterial.Shininess;
            }

            return result;
        }

        private ModelData ProcessMesh(Scene scene, Assimp.Mesh mesh, string? directory)
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

                    if (mesh.HasVertexColors(0))
                    {
                        //// TODO: Let's check if we need to convert from 255 to 0-1.
                        vertex.Color = new Vector4(
                            mesh.VertexColorChannels[0][i].R,
                            mesh.VertexColorChannels[0][i].G,
                            mesh.VertexColorChannels[0][i].B,
                            mesh.VertexColorChannels[0][i].A);
                    }
                    else
                    {
                        vertex.Color = Vector4.One;
                    }

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

            return new ModelData()
            {
                Mesh = new Mesh(this.renderDevice.Factory, vertices.ToArray(), indices.ToArray()),
                Material = this.LoadMaterial(mesh, scene.Materials, directory),
            };
        }

        private IList<ModelData> ProcessNode(Scene scene, Node node, string? directory)
        {
            var meshes = new List<ModelData>();

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
}
