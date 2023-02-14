namespace LittleMars.CameraControls
{
    public class CameraParams
    {
        public float Size { get; private set; }
        public float Height { get; private set; }
        public float Width { get; private set; }

        float _aspect;

        public CameraParams(float aspect)
        {
            _aspect = aspect;
        }

        public void UpdateSize(float size)
        {
            Size = size;
            UpdateHeight();
            UpdateWidth();
        }

        void UpdateHeight() => Height = 2f * Size;
        void UpdateWidth() => Width = Height * _aspect;
    }
}
