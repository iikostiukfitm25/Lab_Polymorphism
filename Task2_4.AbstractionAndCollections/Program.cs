using Task2_4.AbstractionAndCollections;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Для СКРІНШОТУ №3 тимчасово розкоментуй рядок нижче, зафіксуй помилку CS0144 і закоментуй назад:
// var broken = new ShipmentBase("UA-000", 5.0);

List<ShipmentBase> shipments =
[
    new CourierDelivery("UA-201", 3.0, 15.0),
    new LockerDelivery("UA-202", 1.5, 72),
    new FreightDelivery("UA-203", 90.0, true),
    new DroneDelivery("UA-204", 1.8, 150) // Доданий новий тип у Завданні 4
];

Console.WriteLine("=== ЗАВДАННЯ 3: Вибіркова обробка підтипів (is та as) ===");
foreach (ShipmentBase shipment in shipments)
{
    // Оператор is з оголошенням змінної
    if (shipment is LockerDelivery locker)
    {
        Console.WriteLine($"[is] Поштомат №{locker.LockerCellNumber}: PIN-код для отримання = {locker.GeneratePinCode()}");
    }

    // Оператор as з перевіркою на null
    FreightDelivery? freight = shipment as FreightDelivery;
    if (freight is not null)
    {
        Console.WriteLine($"[as] Вантаж {freight.TrackingCode}: {freight.ReserveForklift()}");
    }
}

Console.WriteLine("\n=== ЗАВДАННЯ 4: Узагальнена агрегація та фільтрація OfType<T> ===");
decimal totalCost = CalculateTotalBatchCost(shipments);
Console.WriteLine($"Загальна вартість усієї партії ({shipments.Count} відправлень, включно з дроном): {totalCost} грн");

var couriersOnly = shipments.OfType<CourierDelivery>();
Console.WriteLine($"Кількість кур'єрських доставок: {couriersOnly.Count()}, сумарна відстань: {couriersOnly.Sum(c => c.DistanceKm)} км");

static decimal CalculateTotalBatchCost(IEnumerable<ShipmentBase> batch)
{
    decimal sum = 0m;
    foreach (ShipmentBase s in batch)
    {
        sum += s.CalculateCost(); // Жодного if/switch за конкретними типами
    }
    return sum;
}