﻿using System;
namespace NetQuantities;

/// <summary>
/// Represents a value of electric conductance.
/// This type can be re-interpret-casted into <see cref="double"/> as [S] scale.
/// </summary>
[Quantity]
[QuantityUnit("Siemens", "S", 1.0)]
[QuantityOperation(typeof(QElectricResistance), typeof(QElectricConductance), typeof(QDimensionless))]
public readonly partial struct QElectricConductance : IQuantity
{
}