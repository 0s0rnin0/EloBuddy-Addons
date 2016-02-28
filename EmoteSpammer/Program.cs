﻿using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace EmoteSpammer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Initialize menu
            var menu = MainMenu.AddMenu("EmoteSpammer", "emoteSpammer", "EmoteSpammer - Huuuuuuuu");

            menu.AddGroupLabel("Intro");
            menu.AddLabel("With this emote spammer you can spam any emote while also being able to walk.");
            menu.AddLabel("If you find any stuttering, please adjust the below slider to a higher value.");

            menu.AddSeparator();
            menu.AddGroupLabel("Settings");
            var spamKey = new KeyBind("Spam key", false, KeyBind.BindTypes.HoldActive, 'A', 'U');
            menu.Add("key", spamKey);
            var emoteTypeBox = new ComboBox("Emote type", Enum.GetValues(typeof (Emote)).Cast<Emote>().Select(o => o.ToString()), 2);
            menu.Add("type", emoteTypeBox);
            var delaySlider = new Slider("Spam delay", 75, 50, 150);
            menu.Add("delay", delaySlider);

            // Helpers
            var lastSpam = 0;

            Game.OnUpdate += delegate
            {
                // Key active
                if (spamKey.CurrentValue)
                {
                    if (Core.GameTickCount - lastSpam >= delaySlider.CurrentValue)
                    {
                        // Update last spam
                        lastSpam = Core.GameTickCount;

                        // Do the spamming
                        Player.DoEmote((Emote) Enum.Parse(typeof (Emote), emoteTypeBox.SelectedText));

                        // Instantly move after
                        Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos, false);
                    }
                }
            };
        }
    }
}
