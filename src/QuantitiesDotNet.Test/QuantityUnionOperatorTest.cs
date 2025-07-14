using QuantitiesDotNet.Generic;

namespace QuantitiesDotNet;

public partial class QuantityUnionOperatorTest
{
    [Fact]
    public void UnionToEnergy_NonGeneric()
    {
        var union = new QuantityUnion<QEnergy, QTorque>(1.234);
        var energy = (QEnergy)union;
        Assert.Equal(union.RawValue, energy.RawValue);
    }


    [Fact]
    public void EnergyPerForceIsLength_NonGeneric()
    {
        var energy = new QEnergy(1.234);
        var force = new QForce(5.678);
        var length = energy / force;
        Assert.IsType<QLength>(length);
        Assert.Equal(energy.RawValue / force.RawValue, length.RawValue);
    }


    [Fact]
    public void EnergyPerLengthIsForce_NonGeneric()
    {
        var energy = new QEnergy(1.234);
        var length = new QLength(5.678);
        var force = energy / length;
        Assert.IsType<QForce>(force);
        Assert.Equal(energy.RawValue / length.RawValue, force.RawValue);
    }


    [Fact]
    public void UnionToTorque_NonGeneric()
    {
        var union = new QuantityUnion<QEnergy, QTorque>(1.234);
        var torque = (QTorque)union;
        Assert.Equal(union.RawValue, torque.RawValue);
    }


    [Fact]
    public void TorquePerForceIsLength_NonGeneric()
    {
        var torque = new QTorque(1.234);
        var force = new QForce(5.678);
        var length = torque / force;
        Assert.IsType<QLength>(length);
        Assert.Equal(torque.RawValue / force.RawValue, length.RawValue);
    }


    [Fact]
    public void TorquePerLengthIsForce_NonGeneric()
    {
        var torque = new QTorque(1.234);
        var length = new QLength(5.678);
        var force = torque / length;
        Assert.IsType<QForce>(force);
        Assert.Equal(torque.RawValue / length.RawValue, force.RawValue);
    }


    [Fact]
    public void ForceMultiplyLengthIsEnergyOrTorque_NonGeneric()
    {
        var force = new QForce(1.234);
        var length = new QLength(5.678);
        var union = force * length;
        Assert.IsType<QuantityUnion<QEnergy, QTorque>>(union);
        Assert.Equal(force.RawValue * length.RawValue, union.RawValue);
    }


    [Fact]
    public void LengthMultiplyForceIsEnergyOrTorque_NonGeneric()
    {
        var force = new QForce(1.234);
        var length = new QLength(5.678);
        var union = length * force;
        Assert.IsType<QuantityUnion<QEnergy, QTorque>>(union);
        Assert.Equal(length.RawValue * force.RawValue, union.RawValue);
    }


    [Fact]
    public void UnionToEnergy_Generic()
    {
        var union = new QuantityUnion<decimal, QEnergy<decimal>, QTorque<decimal>>(1.234m);
        var energy = (QEnergy<decimal>)union;
        Assert.Equal(union.RawValue, energy.RawValue);
    }


    [Fact]
    public void UnionToTorque_Generic()
    {
        var union = new QuantityUnion<decimal, QEnergy<decimal>, QTorque<decimal>>(1.234m);
        var torque = (QTorque<decimal>)union;
        Assert.Equal(union.RawValue, torque.RawValue);
    }


    [Fact]
    public void EnergyPerForceIsLength_Generic()
    {
        var energy = new QEnergy<decimal>(1.234m);
        var force = new QForce<decimal>(5.678m);
        var length = energy / force;
        Assert.IsType<QLength<decimal>>(length);
        Assert.Equal(energy.RawValue / force.RawValue, length.RawValue);
    }


    [Fact]
    public void EnergyPerLengthIsForce_Generic()
    {
        var energy = new QEnergy<decimal>(1.234m);
        var length = new QLength<decimal>(5.678m);
        var force = energy / length;
        Assert.IsType<QForce<decimal>>(force);
        Assert.Equal(energy.RawValue / length.RawValue, force.RawValue);
    }


    [Fact]
    public void ForceMultiplyLengthIsEnergyOrTorque_Generic()
    {
        var force = new QForce<decimal>(1.234m);
        var length = new QLength<decimal>(5.678m);
        var union = force * length;
        Assert.IsType<QuantityUnion<decimal, QEnergy<decimal>, QTorque<decimal>>>(union);
        Assert.Equal(force.RawValue * length.RawValue, union.RawValue);
    }


    [Fact]
    public void LengthMultiplyForceIsEnergyOrTorque_Generic()
    {
        var force = new QForce<decimal>(1.234m);
        var length = new QLength<decimal>(5.678m);
        var union = length * force;
        Assert.IsType<QuantityUnion<decimal, QEnergy<decimal>, QTorque<decimal>>>(union);
        Assert.Equal(length.RawValue * force.RawValue, union.RawValue);
    }


    [Fact]
    public void TorquePerForceIsLength_Generic()
    {
        var torque = new QTorque<decimal>(1.234m);
        var force = new QForce<decimal>(5.678m);
        var length = torque / force;
        Assert.IsType<QLength<decimal>>(length);
        Assert.Equal(torque.RawValue / force.RawValue, length.RawValue);
    }


    [Fact]
    public void TorquePerLengthIsForce_Generic()
    {
        var torque = new QTorque<decimal>(1.234m);
        var length = new QLength<decimal>(5.678m);
        var force = torque / length;
        Assert.IsType<QForce<decimal>>(force);
        Assert.Equal(torque.RawValue / length.RawValue, force.RawValue);
    }
}
