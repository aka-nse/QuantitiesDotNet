namespace QuantitiesDotNet
{
    partial class QuantityUnion
    {
        /// <summary>
        /// Casts into <see cref="QEnergy"/>.
        /// </summary>
        /// <param name="union"></param>
        /// <returns></returns>
        public static QEnergy AsEnergy(this QuantityUnion<QEnergy, QTorque> union)
            => new(union.RawValue);

        /// <summary>
        /// Casts into <see cref="QTorque"/>.
        /// </summary>
        /// <param name="union"></param>
        /// <returns></returns>
        public static QTorque AsTorque(this QuantityUnion<QEnergy, QTorque> union)
            => new(union.RawValue);
    }

    readonly partial struct QLength
#if NET7_0_OR_GREATER
        : IMultiplyOperators<QLength, QForce, QuantityUnion<QEnergy, QTorque>>
#endif
    {
        /// <inheritdoc />
        public static QuantityUnion<QEnergy, QTorque> operator *(QLength x, QForce y)
            => new(x.RawValue * y.RawValue);
    }

    readonly partial struct QForce
#if NET7_0_OR_GREATER
        : IMultiplyOperators<QForce, QLength, QuantityUnion<QEnergy, QTorque>>
#endif
    {
        /// <inheritdoc />
        public static QuantityUnion<QEnergy, QTorque> operator *(QForce x, QLength y)
            => new(x.RawValue * y.RawValue);
    }

    readonly partial struct QEnergy
        : IQuantity<QEnergy, double>
#if NET7_0_OR_GREATER
        , IDivisionOperators<QEnergy, QForce, QLength>
        , IDivisionOperators<QEnergy, QLength, QForce>
#endif
    {
        /// <summary>
        /// Casts into <see cref="QEnergy"/>.
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator QEnergy(QuantityUnion<QEnergy, QTorque> x)
            => x.AsEnergy();

        /// <inheritdoc />
        public static QLength operator /(QEnergy x, QForce y)
            => new(x.RawValue / y.RawValue);

        /// <inheritdoc />
        public static QForce operator /(QEnergy x, QLength y)
            => new(x.RawValue / y.RawValue);
    }

    partial struct QTorque
        : IQuantity<QTorque, double>
#if NET7_0_OR_GREATER
        , IDivisionOperators<QTorque, QForce, QLength>
        , IDivisionOperators<QTorque, QLength, QForce>
#endif
    {
        /// <summary>
        /// Casts into <see cref="QTorque"/>.
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator QTorque(QuantityUnion<QEnergy, QTorque> x)
            => x.AsTorque();

        /// <inheritdoc />
        public static QLength operator /(QTorque x, QForce y)
            => new(x.RawValue / y.RawValue);

        /// <inheritdoc />
        public static QForce operator /(QTorque x, QLength y)
            => new(x.RawValue / y.RawValue);
    }
}

#if NET7_0_OR_GREATER
namespace QuantitiesDotNet.Generic
{
    partial class QuantityUnion
    {
        /// <summary>
        /// Casts into <see cref="QEnergy"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="union"></param>
        /// <returns></returns>
        public static QEnergy<T> AsEnergy<T>(this QuantityUnion<T, QEnergy<T>, QTorque<T>> union)
            where T : INumber<T>
            => new(union.RawValue);

        /// <summary>
        /// Casts into <see cref="QTorque"/>.
        /// </summary>
        /// <param name="union"></param>
        /// <returns></returns>
        public static QTorque<T> AsTorque<T>(this QuantityUnion<T, QEnergy<T>, QTorque<T>> union)
            where T : INumber<T>
            => new(union.RawValue);
    }

    readonly partial struct QLength<T>
#if NET7_0_OR_GREATER
        : IMultiplyOperators<QLength<T>, QForce<T>, QuantityUnion<T, QEnergy<T>, QTorque<T>>>
#endif
    {
        /// <inheritdoc />
        public static QuantityUnion<T, QEnergy<T>, QTorque<T>> operator *(QLength<T> x, QForce<T> y)
            => new(x.RawValue * y.RawValue);
    }

    readonly partial struct QForce<T>
#if NET7_0_OR_GREATER
        : IMultiplyOperators<QForce<T>, QLength<T>, QuantityUnion<T, QEnergy<T>, QTorque<T>>>
#endif
    {
        /// <inheritdoc />
        public static QuantityUnion<T, QEnergy<T>, QTorque<T>> operator *(QForce<T> x, QLength<T> y)
            => new(x.RawValue * y.RawValue);
    }

    readonly partial struct QEnergy<T>
        : IQuantity<QEnergy<T>, T>
#if NET7_0_OR_GREATER
        , IDivisionOperators<QEnergy<T>, QForce<T>, QLength<T>>
        , IDivisionOperators<QEnergy<T>, QLength<T>, QForce<T>>
#endif
    {
        /// <summary>
        /// Casts into <see cref="QEnergy"/>.
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator QEnergy<T>(QuantityUnion<T, QEnergy<T>, QTorque<T>> x)
            => x.AsEnergy();

        /// <inheritdoc />
        public static QLength<T> operator /(QEnergy<T> x, QForce<T> y)
            => new(x.RawValue / y.RawValue);

        /// <inheritdoc />
        public static QForce<T> operator /(QEnergy<T> x, QLength<T> y)
            => new(x.RawValue / y.RawValue);
    }

    readonly partial struct QTorque<T>
        : IQuantity<QTorque<T>, T>
#if NET7_0_OR_GREATER
        , IDivisionOperators<QTorque<T>, QForce<T>, QLength<T>>
        , IDivisionOperators<QTorque<T>, QLength<T>, QForce<T>>
#endif
    {
        /// <summary>
        /// Casts into <see cref="QTorque{T}"/>.
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator QTorque<T>(QuantityUnion<T, QEnergy<T>, QTorque<T>> x)
            => x.AsTorque();

        /// <inheritdoc />
        public static QLength<T> operator /(QTorque<T> x, QForce<T> y)
            => new(x.RawValue / y.RawValue);

        /// <inheritdoc />
        public static QForce<T> operator /(QTorque<T> x, QLength<T> y)
            => new(x.RawValue / y.RawValue);
    }
}
#endif