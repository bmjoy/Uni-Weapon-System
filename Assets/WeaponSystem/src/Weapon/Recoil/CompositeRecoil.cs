using UnityEngine;

namespace WeaponSystem.Weapon.Recoil
{
    [System.Serializable]
    public class CompositeRecoil : IRecoil
    {
        [SerializeField] private SinRandomRecoil sinRandomRecoil;
        [SerializeField] private PatternRecoil patternRecoil;
        public void Reset()
        {
            patternRecoil.Reset();
            sinRandomRecoil.Reset();
        }

        public void Generate()
        {
            sinRandomRecoil.Generate();
            patternRecoil.Generate();
        }

        public void Easing()
        {
            sinRandomRecoil.Easing();
            patternRecoil.Easing();
        }
    }
}