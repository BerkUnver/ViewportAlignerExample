/*

*/
using Godot;
using System;
using Yeast;
namespace Yeast.GDAddon.Viewport
{
    class VSnap : Godot.Viewport
    {
        private Vector2 _position;

        [Signal]
        public delegate void position_changed();
        public VSnap()
        // This should be in the initializer and not the ready function because it serves to "initalize" _position and _roundedPosition.
        {
            Position = CanvasTransform.origin;
            // This gets rid of any fractional values in the origin vector and sets _realPosition. 
            // It causes an errror (not a crash) when placed in the initializer, however. 
        }
        public override void _EnterTree()
        // This is a bug workaround, details on the bug here:
        // https://github.com/godotengine/godot/issues/50379
        {
            CanvasTransform = CanvasTransform;
        }
        public Vector2 BorderlessSize
        {
            get
            {
                return new Vector2(Size.x - 2, Size.y - 2);
            }
        }
        public Vector2 BorderlessPosition
        {
            get
            {
                return new Vector2(_position.x + 1, _position.y + 1);
            }
            set
            {
                Position = new Vector2(value.x - 1, value.y - 1);
            }
        }
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                Vector2 _oldPosition = _position;
                // GD.Print("old position: " + _oldPosition);
                // GD.Print("new position: " + value);
                _position = value;
                float _roundedX = (float) Math.Round(value.x);
                float _roundedY = (float) Math.Round(value.y);
                Vector2 _roundedPosition = new Vector2(_roundedX, _roundedY);

                CanvasTransform = new Transform2D(new Vector2(1, 0), new Vector2(0, 1), _roundedPosition);
                if (!(_oldPosition == _position))
                // If the position has actually changed (This is an optimization to prevent the signal from being falsely emitted.)
                // This has some drawbacks. For example, if a parallax coefficient is changed in the editor while the game is running,
                // it will only update when the camera moves because this signal will be emitted and its position will be multiplied by the new parallax value.
                {
                    EmitSignal(nameof(position_changed));
                }
            }
        }

        public Vector2 RoundedPosition
        {
            get
            {
                return CanvasTransform.origin;
                // This is get-only because you should set the position as Position and then it will autoround.
            }
        }
    }
}