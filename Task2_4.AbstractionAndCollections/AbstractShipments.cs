namespace Task2_4.AbstractionAndCollections;

public abstract class ShipmentBase
{
    public string TrackingCode { get; }
    public double WeightKg { get; }

    protected ShipmentBase(string trackingCode, double weightKg)
    {
        if (string.IsNullOrWhiteSpace(trackingCode))
            throw new ArgumentException("Трек-номер не може бути порожнім.", nameof(trackingCode));
        if (weightKg <= 0)
            throw new ArgumentOutOfRangeException(nameof(weightKg), "Вага має бути додатною.");

        TrackingCode = trackingCode.Trim().ToUpperInvariant();
        WeightKg = weightKg;
    }

    public abstract string ServiceCategory { get; }
    public abstract decimal CalculateCost();
    public abstract string GetSummary();
    public virtual int GetEstimatedDays() => 3;
}

public class CourierDelivery : ShipmentBase
{
    public double DistanceKm { get; }
    public CourierDelivery(string trackingCode, double weightKg, double distanceKm)
        : base(trackingCode, weightKg) => DistanceKm = distanceKm;

    public override string ServiceCategory => "Кур'єр";
    public override decimal CalculateCost() => 50m + (decimal)WeightKg * 10m + (decimal)DistanceKm * 4m;
    public override string GetSummary() => $"[{TrackingCode}] Кур'єр ({WeightKg} кг, {DistanceKm} км)";
    public override int GetEstimatedDays() => 1;
}

public class LockerDelivery : ShipmentBase
{
    public int LockerCellNumber { get; }
    public LockerDelivery(string trackingCode, double weightKg, int cellNumber)
        : base(trackingCode, weightKg) => LockerCellNumber = cellNumber;

    public override string ServiceCategory => "Поштомат";
    public override decimal CalculateCost() => 45m + (decimal)WeightKg * 5m;
    public override string GetSummary() => $"[{TrackingCode}] Поштомат №{LockerCellNumber} ({WeightKg} кг)";

    // Унікальний метод підтипу (для перевірки оператора is)
    public string GeneratePinCode() => $"{LockerCellNumber * 17 % 9000 + 1000}";
}

public class FreightDelivery : ShipmentBase
{
    public bool RequiresPallet { get; }
    public FreightDelivery(string trackingCode, double weightKg, bool requiresPallet)
        : base(trackingCode, weightKg) => RequiresPallet = requiresPallet;

    public override string ServiceCategory => "Вантаж";
    public override decimal CalculateCost() => 150m + (decimal)WeightKg * 18m + (RequiresPallet ? 120m : 0m);
    public override string GetSummary() => $"[{TrackingCode}] Вантаж ({WeightKg} кг, палета: {(RequiresPallet ? "так" : "ні")})";
    public override int GetEstimatedDays() => 5;

    // Унікальний метод підтипу (для перевірки оператора as)
    public string ReserveForklift() => RequiresPallet ? "Навантажувач зарезервовано" : "Ручне розвантаження";
}

// Новий 4-й клас для Завдання 4 (перевірка розширюваності без зміни узагальненого коду)
public class DroneDelivery : ShipmentBase
{
    public int FlightAltitudeMeters { get; }
    public DroneDelivery(string trackingCode, double weightKg, int altitude)
        : base(trackingCode, weightKg) => FlightAltitudeMeters = altitude;

    public override string ServiceCategory => "Авіа-дрон";
    public override decimal CalculateCost() => 120m + (decimal)WeightKg * 25m;
    public override string GetSummary() => $"[{TrackingCode}] Дрон ({WeightKg} кг, висота {FlightAltitudeMeters} м)";
    public override int GetEstimatedDays() => 0;
}