using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AntonHat
{
    internal static class Anton
    {
        internal static readonly string sprFront = "Anton Skull Hat Front";
        internal static readonly string sprBack = "Anton Skull Hat Back";
        internal static readonly string sprOW = "Anton Skull Hat Overworld";

        internal static void Init()
        {
            CustomTextures.CreateSprite(sprFront, "AntonHat/front.png");
            CustomTextures.CreateSprite(sprBack, "AntonHat/back.png");
            CustomTextures.CreateSprite(sprOW, "AntonHat/overworld.png", new Vector2(0.5f, 0));

        } // end Init

    } // end class Anton

} // end namespace AntonHat