namespace PostalService.Api.Models
{
    public class InputArgs
    {
        public InputArgs(int weight, int height, int width, int depth)
        {
            Weight = weight; Height = height; Width = width; Depth = depth;
        }
        public int Weight { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int Depth { get; private set; }
    }
}
