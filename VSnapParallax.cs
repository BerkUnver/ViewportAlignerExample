using Godot;

namespace Yeast.GDAddon.Viewport
{
    class VSnapParallax : VSnap
    {
        [Export]
        private float _parallaxCoefficient = 1f;
        public Vector2 ParallaxPosition
        {
            get
            {
                return Position * _parallaxCoefficient;
            }
            set
            {
                Position = value * _parallaxCoefficient;
            }
        }
    }
}