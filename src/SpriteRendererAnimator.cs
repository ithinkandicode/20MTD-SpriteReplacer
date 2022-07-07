using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static AssetReplacer.AssetReplacer;
using AssetReplacer.AssetStore;

namespace AssetReplacer
{
    public class SpriteRendererAnimator : MonoBehaviour
    {
        internal SpriteRenderer target;
        internal List<Sprite> frames;
        private int fps = 2;
        private int loopDelay = 0;
        public bool active;
        private IEnumerator animation;

        void Awake()
        {
            target = this.GetComponentInParent<SpriteRenderer>();
            this.frames = SpriteAnimationStore.SpriteAnimationDict[target.name.Replace("(Clone)", "")];
        }

        void OnEnable()
        {
            if (target is not null)
            {
                this.active = true;
                animation = this.Animate();
                base.StartCoroutine(animation);
            }
            else
            {
                Log.LogError("Failed to animate " + this.transform.parent.gameObject.name);
            }
        }

        void OnDisable()
        {
            this.active = false;
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
                    yield return new WaitForSecondsRealtime(1f / this.fps);
                }
                yield return new WaitForSecondsRealtime(loopDelay);
            }
        }
    }
}