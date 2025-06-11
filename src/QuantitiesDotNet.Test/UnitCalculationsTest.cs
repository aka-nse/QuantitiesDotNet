namespace QuantitiesDotNet;

public class UnitCalculationsTest
{
    [Fact]
    public void Test()
    {
        //This test is implemented to verify the definition of the operator.
        // Make sure the build is successful.
        var accel = QAcceleration.FromMetrePerSquareSecond(1);
        var angle = QAngle.FromRadian(1);
        var angac = QAngularAcceleration.FromRadianPerSquareSecond(1);
        var angvl = QAngularVelocity.FromRadianPerSecond(1);
        var area_ = QArea.FromMetre2(1);
        var catal = QCatalyticActivity.FromKatal(1);
        var denst = QDensity.FromKilogramPerCubicMetre(1);
        var nodim = QDimensionless.FromRaw(1);
        var ecapa = QElectricCapacity.FromFarad(1);
        var echrg = QElectricCharge.FromCoulomb(1);
        var econd = QElectricConductance.FromSiemens(1);
        var econy = QElectricConductivity.FromSiemensPerMetre(1);
        var ecurr = QElectricCurrent.FromAmpere(1);
        var eflds = QElectricFieldStrength.FromVoltPerMetre(1);
        var eflxd = QElectricFluxDensity.FromCoulombPerSquareMetre(1);
        var eidtc = QElectricIndactance.FromHenry(1);
        var eresi = QElectricResistance.FromOhm(1);
        var evolt = QElectricVoltage.FromVolt(1);
        var energ = QEnergy.FromJoule(1);
        var force = QForce.FromNewton(1);
        var frequ = QFrequency.FromHertz(1);
        var hcapa = QHeatCapacity.FromJoulePerKelvin(1);
        var lngth = QLength.FromMetre(1);
        var lumin = QLuminance.FromCandelaPerSquareMetre(1);
        var lumit = QLuminousIntensity.FromCandela(1);
        var mflds = QMagneticFieldStrength.FromAmparePerMetre(1);
        var mflux = QMagneticFlux.FromWeber(1);
        var mflxd = QMagneticFluxDensity.FromTesla(1);
        var mass_ = QMass.FromKilogram(1);
        var power = QPower.FromWatt(1);
        var press = QPressure.FromPascal(1);
        var shcap = QSpecificHeatCapacity.FromJoulePerKilogramPerKelvin(1);
        var subst = QSubstanceAmount.FromMole(1);
        var tmpre = QTemperature.FromKelvin(1);
        var time_ = QTime.FromSecond(1);
        var torqu = QTorque.FromNewtonMetre(1);
        var speed = QSpeed.FromMetrePerSecond(1);
        var volum = QVolume.FromMetre3(1);

        Assert.Equal(time_ * accel, speed);
        Assert.Equal(time_ * angac, angvl);
        Assert.Equal(time_ * angvl, angle);
        Assert.Equal(lngth * lngth, area_);
        Assert.Equal(time_ * catal, subst);
        Assert.Equal(volum * denst, mass_);
        Assert.Equal(ecapa * evolt, echrg);
        Assert.Equal(ecurr * time_, echrg);
        Assert.Equal(eidtc * ecurr, mflux);
        Assert.Equal(eresi * econd, nodim);
        Assert.Equal(lngth * econy, econd);
        Assert.Equal(lngth * eflds, evolt);
        Assert.Equal(area_ * eflxd, echrg);
        Assert.Equal(ecurr * eresi, evolt);
        Assert.Equal(mass_ * accel, force);
        Assert.Equal(time_ * frequ, nodim);
        Assert.Equal(tmpre * hcapa, energ);
        Assert.Equal(area_ * lumin, lumit);
        Assert.Equal(lngth * mflds, ecurr);
        Assert.Equal(area_ * mflxd, mflux);
        Assert.Equal(time_ * power, energ);
        Assert.Equal(ecurr * evolt, power);
        Assert.Equal(area_ * press, force);
        Assert.Equal(mass_ * shcap, hcapa);
        Assert.Equal(time_ * speed, lngth);
        Assert.Equal(lngth * area_, volum);

        // energy and torque have same quantity dimension,
        // so operator should support switching for each type.
        Assert.Equal(lngth * force, energ);
        Assert.Equal(lngth * force, torqu);
    }
}