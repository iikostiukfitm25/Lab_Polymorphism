using Task1.VirtualPolymorphism;

Console.OutputEncoding = System.Text.Encoding.UTF8;

List<DeliveryShipment> shipments =
[
    new CourierDelivery("UA-101", 2.5, 12.0),
    new LockerDelivery("UA-102", 1.2, 48),
    new FreightDelivery("UA-103", 85.0, true)
];

foreach (DeliveryShipment item in shipments)
{
    Console.WriteLine($"Тип у купі: {item.GetType().Name}");
    Console.WriteLine($"  Опис: {item.GetSummary()}");
    Console.WriteLine($"  Вартість: {item.CalculateCost()} грн | Термін: {item.GetEstimatedDays()} дн.\n");
}