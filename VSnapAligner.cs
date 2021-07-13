using Godot;
using System;
using Yeast;

namespace Yeast.GDAddon.Viewport
{
    class VSnapAligner : TextureRect
    {
        private VSnap _vSnap;
        public VSnap Viewport
        {
            get
            {
                return _vSnap;
            }
        }
        public override void _Ready()
        {
            GD.Print(((ViewportTexture) Texture).ViewportPath);
            _vSnap = (VSnap) Owner.GetNode(((ViewportTexture) Texture).ViewportPath);
            // _vSnap = (VSnap) GetChild(0);
            _vSnap.Connect("position_changed", this, nameof(_on_position_changed));
            // ASsumes that this node has only one child and it is the viewportsnap this is managing
        }
        public void _on_position_changed()
        {
            Vector2 _sizeScale = RectSize / _vSnap.BorderlessSize;
            Vector2 _offset = (_vSnap.Position - _vSnap.RoundedPosition) * _sizeScale;

            MarginLeft = _offset.x - _sizeScale.x;
            MarginRight = _offset.x + _sizeScale.y;
            MarginTop = _offset.y - _sizeScale.x;
            MarginBottom = _offset.y + _sizeScale.y;        
        }
    }
}