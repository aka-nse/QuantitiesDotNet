namespace QuantitiesDotNet.Generators;

partial class Generator
{
    private const string AttributesText = """
        using System;

        namespace QuantitiesDotNet;


        [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
        internal sealed class QuantityAttribute : Attribute
        {
            public int L { get; }
            public int M { get; }
            public int T { get; }
            public int I { get; }
            public int Th { get; }
            public int N { get; }
            public int J { get; }

            public QuantityAttribute(int L, int M, int T, int I, int Th, int N, int J)
            {
                this.L  = L ;
                this.M  = M ;
                this.T  = T ;
                this.I  = I ;
                this.Th = Th;
                this.N  = N ;
                this.J  = J ;
            }
        }

        [AttributeUsage(AttributeTargets.Struct, AllowMultiple = true)]
        internal sealed class QuantityUnitAttribute : Attribute
        {
            public string Name { get; }
            public string Unit { get; }
            public double Scale { get; }
            public UnitPrefix Prefix { get; }
            public int PowerOfPrefix { get; }
            public bool ExportsShorthandSymbol { get; }

            /// <summary>
            /// Creates a new instance of <see cref="QuantityUnitAttribute"/>.
            /// </summary>
            /// <param name="name"> The name of unit. </param>
            /// <param name="unit"> The simply unit symbol expression in ASCII. </param>
            /// <param name="scale">
            ///     <para>The scale of the unit from raw value.</para>
            ///     <para>e.g.: `scale` for "gram" is 1e-3, because raw value is killogram</para>
            /// </param>
            /// <param name="prefix"> The set of available SI prefix. </param>
            /// <param name="powerOfPrefix">
            ///     <para>The coefficient for SI prefix exponent.</para>
            ///     <para>
            ///         e.g.
            ///         <list type="table">
            ///             <item>
            ///                 <term>for <see cref="QSpatialFrequency" /></term>
            ///                 <description>-1</description>
            ///             </item>
            ///             <item>
            ///                 <term>for <see cref="QLength" /></term>
            ///                 <description>1</description>
            ///             </item>
            ///             <item>
            ///                 <term>for <see cref="QArea" /></term>
            ///                 <description>2</description>
            ///             </item>
            ///             <item>
            ///                 <term>for <see cref="QVolume" /></term>
            ///                 <description>3</description>
            ///             </item>
            ///         </list>
            ///     </para>
            /// </param>
            /// <param name="exportsShorthandSymbol">
            ///     <para>If <c>true</c> generates symbol field for unit shorthands into <see cref="QuantitiesDotNet.UnitShorthands.UnitExtensions" />:</para>
            ///     <para>otherwise <c>false</c>.</para>
            /// </param>
            public QuantityUnitAttribute(
                string name,
                string unit,
                double scale,
                UnitPrefix prefix = UnitPrefix.None,
                int powerOfPrefix = 1,
                bool exportsShorthandSymbol = false)
            {
                Name = name;
                Unit = unit;
                Scale = scale;
                Prefix = prefix;
                PowerOfPrefix = powerOfPrefix;
                ExportsShorthandSymbol = exportsShorthandSymbol;
            }
        }


        [AttributeUsage(AttributeTargets.Struct, AllowMultiple = true)]
        internal sealed class QuantityOperationAttribute : Attribute
        {
            public Type MultiplicantType { get; }
            public Type MultiplierType { get; }
            public Type ProductType { get; }

            public QuantityOperationAttribute(Type multiplicant, Type multiplier, Type product)
            {
                MultiplicantType = multiplicant;
                MultiplierType = multiplier;
                ProductType = product;
            }
        }


        [Flags]
        internal enum UnitPrefix
        {
            None = 0,

            Centi  = 1 << 0,
            Milli  = 1 << 1,
            Micro  = 1 << 2,
            Nano   = 1 << 3,
            Pico   = 1 << 4,
            Femto  = 1 << 5,
            Atto   = 1 << 6,
            Zepto  = 1 << 7,
            Yocto  = 1 << 8,
            Ronto  = 1 << 9,
            Quecto = 1 << 10,

            Hecto  = 1 << (0 + 16),
            Kilo   = 1 << (1 + 16),
            Mega   = 1 << (2 + 16),
            Giga   = 1 << (3 + 16),
            Tera   = 1 << (4 + 16),
            Peta   = 1 << (5 + 16),
            Exa    = 1 << (6 + 16),
            Zetta  = 1 << (7 + 16),
            Yotta  = 1 << (8 + 16),
            Ronna  = 1 << (9 + 16),
            Quetta = 1 << (10 + 16),
        }
        """;
}
