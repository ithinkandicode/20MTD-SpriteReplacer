using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer
{
    public class SpriteRendererAnimator : MonoBehaviour
    {
        private SpriteRenderer target;
        private List<Sprite> frames;
        private int fps = 10;
        private bool active;
        private IEnumerator animation;

        void Awake()
        {
            target = this.GetComponentInParent<SpriteRenderer>();
        }

        void OnEnable()
        {
            if (target is not null)
            {
                animation = Animate();
                base.StartCoroutine(animation);
            }
            else
            {
                Log.LogError("Failed to animate " + this.transform.parent.gameObject.name);
            }
        }

        void OnDisable()
        {
            base.StopCoroutine(this.animation);
        }

        private IEnumerator Animate()
        {
            while (active)
            {
                for (int i = 0; i < frames.Count; i++)
                {
                    target.sprite = frames[i];
                    if (!active)
                    {
                        yield break;
                    }
                }
            }
        }
    }
}