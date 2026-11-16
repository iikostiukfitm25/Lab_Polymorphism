namespace Task1.VirtualPolymorphism;

public class DeliveryShipment
{
    public string TrackingCode { get; }
    public double WeightKg { get; }

    public DeliveryShipment(string trackingCode, double weightKg)
    {
        if (weightKg <= 0) throw new ArgumentOutOfRangeException(nameof(weightKg));
        TrackingCode = trackingCode;
        WeightKg = weightKg;
    }

    public virtual decimal CalculateCost() => 50m + (decimal)WeightKg * 10m;
    public virtual string GetSummary() => $"[{TrackingCode}] Базове відправлення ({WeightKg} кг)";
    public virtual int GetEstimatedDays() => 3;
}

public class CourierDelivery : DeliveryShipment
{
    public double DistanceKm { get; }
    public CourierDelivery(string trackingCode, double weightKg, double distanceKm)
        : base(trackingCode, weightKg) => DistanceKm = distanceKm;

    public override decimal CalculateCost() => base.CalculateCost() + (decimal)DistanceKm * 4m;
    public override string GetSummary() => $"[{TrackingCode}] Кур'єрська доставка ({WeightKg} кг, {DistanceKm} км)";
    public override int GetEstimatedDays() => 1;
}

public class LockerDelivery : DeliveryShipment
{
    public int LockerCellNumber { get; }
    public LockerDelivery(string trackingCode, double weightKg, int cellNumber)
        : base(trackingCode, weightKg) => LockerCellNumber = cellNumber;

    public override decimal CalculateCost() => 45m + (decimal)WeightKg * 5m;
    public override string GetSummary() => $"[{TrackingCode}] Поштомат №{LockerCellNumber} ({WeightKg} кг)";
    // GetEstimatedDays() НЕ перевизначено — лишається базова поведінка (3 дні)
}

public class FreightDelivery : DeliveryShipment
{
    public bool RequiresPallet { get; }
    public FreightDelivery(string trackingCode, double weightKg, bool requiresPallet)
        : base(trackingCode, weightKg) => RequiresPallet = requiresPallet;

    public override decimal CalculateCost() => 150m + (decimal)WeightKg * 18m + (RequiresPallet ? 120m : 0m);
    public override string GetSummary() => $"[{TrackingCode}] Вантажне перевезення ({WeightKg} кг, палета: {(RequiresPallet ? "так" : "ні")})";
    public override int GetEstimatedDays() => 5;
}