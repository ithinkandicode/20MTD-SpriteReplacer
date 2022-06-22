using System;
using System.IO;
using System.Text;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using flanne.UI;

namespace SpriteReplacer
{
    class PatchPanels
    {
        [HarmonyPatch(typeof(Panel), "Start")]
        [HarmonyPrefix]
        static void StartPrefix(ref Panel __instance)
        {			
			//Console.WriteLine("");
			//Console.WriteLine("[~MODS~] PANEL_LOG: START");

			CanvasGroup[] canvasComponentsInChildren = __instance.GetComponentsInChildren<CanvasGroup>();

			for (int i = 0; i < canvasComponentsInChildren.Length; i++)
			{
				GameObject currObject = canvasComponentsInChildren[i].gameObject;
				//Console.WriteLine("[~MODS~] PANEL:" + currObject.name);

				foreach (Image currImg in currObject.GetComponentsInChildren<Image>())
				{
					//Console.WriteLine("[~MODS~] PANEL_Image:" + currImg.name);

					Utils.ReplaceSpriteTexture(currImg.sprite);

					/*
					if (currImg.sprite != null)
					{
						Console.WriteLine("[~MODS~] PANEL_Image:Sprite:BASE===:" + currImg.sprite.name);
						Texture2D spriteTexture = currImg.sprite.texture;
						if (spriteTexture != null)
						{
							Console.WriteLine("[~MODS~] PANEL_Image:Sprite:Texture:" + spriteTexture.name);
							string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "texturemods", spriteTexture.name);
							Console.WriteLine("[~MODS~] SearchPath:" + path + ".png");
							if (File.Exists(path + ".png"))
							{
								Sprite ogSprite = currImg.sprite;
								ogSprite.texture.LoadImage(File.ReadAllBytes(path + ".png"));
								Vector2 standardisedPivot = new Vector2(ogSprite.pivot.x / ogSprite.rect.width, ogSprite.pivot.y / ogSprite.rect.height);
								Sprite sprite = Sprite.Create(ogSprite.texture, ogSprite.rect, standardisedPivot, ogSprite.pixelsPerUnit);
								sprite.name = ogSprite.name;
								spriteTexture = sprite.texture;
							}
						}
					}
					*/
				}
			}
			//Console.WriteLine("[~MODS~] PANEL_LOG: END");


		}
	}
}
