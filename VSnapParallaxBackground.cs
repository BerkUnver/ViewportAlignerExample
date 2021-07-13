using Godot;
using System;
using System.Collections.Generic;


// This aligns all children ViewportSnapAligner's ViewportSnaps to have the same transform
namespace Yeast.GDAddon.Viewport
{
    class VSnapParallaxBackground : Control
    {
        private List<VSnapParallax> _parallaxLayers =  new List<VSnapParallax>();
        public override void _Ready()
        {
            Godot.Collections.Array _children = GetChildren();
            foreach (Node _child in _children)
            {
                if (_child is VSnapAligner)
                {
                    VSnapAligner _aligner = (VSnapAligner) _child;

                    if (_aligner.Viewport is VSnapParallax)
                    // Does this need to be here? It seems kind of redundant because this should already be assured.
                    {
                        _parallaxLayers.Add((VSnapParallax) _aligner.Viewport);
                    }
                }
            }
        }
        public Vector2 Position
        {
            set
            {
                foreach (VSnapParallax _layer in _parallaxLayers)
                {
                    _layer.ParallaxPosition = value;
                }
            }
        }
    }
}