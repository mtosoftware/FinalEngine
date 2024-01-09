namespace FinalEngine.Rendering.Textures;

public struct  CubeTextureDescription
{
    private TextureFilterMode? magFilter;

    private TextureFilterMode? minFilter;

    private TextureWrapMode? wrapS;

    private TextureWrapMode? wrapT;

    private TextureWrapMode? wrapR;

    public bool GenerateMipmaps { get; set; }

    public int Height { get; set; }

    public TextureFilterMode MagFilter
    {
        readonly get { return this.magFilter ?? TextureFilterMode.Linear; }
        set { this.magFilter = value; }
    }

    public TextureFilterMode MinFilter
    {
        readonly get { return this.minFilter ?? TextureFilterMode.Linear; }
        set { this.minFilter = value; }
    }
    public int Width { get; set; }

    public TextureWrapMode WrapS
    {
        readonly get { return this.wrapS ?? TextureWrapMode.Clamp; }
        set { this.wrapS = value; }
    }

    public TextureWrapMode WrapT
    {
        readonly get { return this.wrapT ?? TextureWrapMode.Clamp; }
        set { this.wrapT = value; }
    }
    public TextureWrapMode WrapR
    {
        readonly get { return this.wrapR ?? TextureWrapMode.Clamp; }
        set { this.wrapR = value; }
    }

    public static bool operator !=(CubeTextureDescription left, CubeTextureDescription right)
    {
        return !(left == right);
    }

    public static bool operator ==(CubeTextureDescription left, CubeTextureDescription right)
    {
        return left.Equals(right);
    }

    public readonly bool Equals(CubeTextureDescription other)
    {
        return this.MinFilter == other.MinFilter &&
               this.MagFilter == other.magFilter &&
               this.WrapS == other.WrapS &&
               this.WrapT == other.WrapT &&
               this.WrapR == other.WrapR &&
               this.Width == other.Width &&
               this.Height == other.Height;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is CubeTextureDescription description && this.Equals(description);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return (this.MinFilter.GetHashCode() * accumulator) +
               (this.MagFilter.GetHashCode() * accumulator) +
               (this.WrapS.GetHashCode() * accumulator) +
               (this.WrapT.GetHashCode() * accumulator) +
               (this.WrapR.GetHashCode() * accumulator) +
               (this.Width.GetHashCode() * accumulator) +
               (this.Height.GetHashCode() * accumulator);
    }
}
