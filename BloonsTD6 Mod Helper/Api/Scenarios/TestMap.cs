﻿using Assets.Scripts.Data.MapSets;
using Assets.Scripts.Models.Map;
using Assets.Scripts.Simulation.SMath;
using Assets.Scripts.Utils;
using System.Collections.Generic;

namespace BTD_Mod_Helper.Api.Scenarios
{
    internal class TestMap : ModMap
    {
        public override string Name => "Test Map";
        public override MapDifficulty Difficulty => MapDifficulty.Beginner;
        public override SpriteReference MapSprite => GetSpriteReference<MelonMain>(Name);

        public TestMap()
        {
            AddPath(new List<Vector2>()
            {
                new Vector2(-48.70371f, -113.0834f),
                new Vector2(-45.00001f, 23.85197f)
            });

            AddAreaModel(AreaType.water, new List<Vector2>()
            {
                new Vector2(3, 5),
                new Vector2(3, 5),
                new Vector2(3, 5),
                new Vector2(3, 5),
            });
        }
    }
}
