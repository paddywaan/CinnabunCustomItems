using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Backpack
{
    public class BoneSetter : MonoBehaviour
    {
        [SerializeField]
        private Transform skeletonRoot;
 
        [SerializeField]
        private string[] boneNames;
 
        [SerializeField]
        private Transform[] bones;
 
        //[ContextMenu("GetBoneNames()")]
        public void GetBoneNames()
        {
            SkinnedMeshRenderer srenderer = GetComponent<SkinnedMeshRenderer>();
            if(srenderer == null)
                return;
 
            Transform[] bones = srenderer.bones;
 
            boneNames = new string[bones.Length];
 
            for(int i = 0; i < bones.Length; i++)
                boneNames[i] = bones[i].name;
        }
 
 
 
        //[ContextMenu("SetBones()")]
        public void SetBones()
        {
            if(skeletonRoot == null)
            {
                Debug.LogError("Root object is not set!");
                return;
            }
 
            bones = new Transform[boneNames.Length];
         
            for(int i = 0; i < boneNames.Length; i++)
            {
                bones[i] = skeletonRoot.FindInChildren(boneNames[i]);
            }
         
            SkinnedMeshRenderer srenderer = GetComponent<SkinnedMeshRenderer>();
         
            srenderer.bones = bones;
 
            srenderer.rootBone = skeletonRoot;
        }
 
 
        void Awake()
        {
            SetBones();
        }
    }
}