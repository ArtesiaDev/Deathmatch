using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Core.GameEntity
{
    [Serializable]
    public struct Proportion
    {
        public Proportion(int fractionCount, int totalNumberOfParts, int fraction1, int fraction2, int fraction3,
            int fraction4, int fraction5, int fraction6, int fraction7, int fraction8, int fraction9, int fraction10)
        {
            FractionCount = fractionCount;
            TotalNumberOfParts = totalNumberOfParts;
            Fraction1 = fraction1;
            Fraction2 = fraction2;
            Fraction3 = fraction3;
            Fraction4 = fraction4;
            Fraction5 = fraction5;
            Fraction6 = fraction6;
            Fraction7 = fraction7;
            Fraction8 = fraction8;
            Fraction9 = fraction9;
            Fraction10 = fraction10;
        }

        [field: SerializeField, Range(2, 10)] public int FractionCount { get; private set; }
        [field: SerializeField] public int TotalNumberOfParts { get; private set; }

        [field: SerializeField, Space(20)] public int Fraction1 { get; private set; }
        [field: SerializeField] public int Fraction2 { get; private set; }

        [field: SerializeField, ShowIf("@FractionCount > 2")]
        public int Fraction3 { get; private set; }

        [field: SerializeField, ShowIf("@FractionCount > 3")]
        public int Fraction4 { get; private set; }

        [field: SerializeField, ShowIf("@FractionCount > 4")]
        public int Fraction5 { get; private set; }

        [field: SerializeField, ShowIf("@FractionCount > 5")]
        public int Fraction6 { get; private set; }

        [field: SerializeField, ShowIf("@FractionCount > 6")]
        public int Fraction7 { get; private set; }

        [field: SerializeField, ShowIf("@FractionCount > 7")]
        public int Fraction8 { get; private set; }

        [field: SerializeField, ShowIf("@FractionCount > 8")]
        public int Fraction9 { get; private set; }

        [field: SerializeField, ShowIf("@FractionCount > 9")]
        public int Fraction10 { get; private set; }

        public float Fraction1Chance => GetProportionFromFraction(Fraction1);
        public float Fraction2Chance => GetProportionFromFraction(Fraction2);
        public float Fraction3Chance => GetProportionFromFraction(Fraction3);
        public float Fraction4Chance => GetProportionFromFraction(Fraction4);
        public float Fraction5Chance => GetProportionFromFraction(Fraction5);
        public float Fraction6Chance => GetProportionFromFraction(Fraction6);
        public float Fraction7Chance => GetProportionFromFraction(Fraction7);
        public float Fraction8Chance => GetProportionFromFraction(Fraction8);
        public float Fraction9Chance => GetProportionFromFraction(Fraction9);
        public float Fraction10Chance => GetProportionFromFraction(Fraction10);

        public float GetProportionFromFraction(int fraction) =>
            (float)fraction / TotalNumberOfParts;
    }
}