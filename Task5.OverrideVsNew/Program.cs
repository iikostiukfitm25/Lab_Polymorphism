using Task5.OverrideVsNew;

Console.OutputEncoding = System.Text.Encoding.UTF8;

BaseNotifier baseRef = new DerivedNotifier();
DerivedNotifier derivedRef = new DerivedNotifier();

Console.WriteLine($"Фактичний тип об'єкта в купі (GetType): {baseRef.GetType().Name}\n");

Console.WriteLine("--- Сценарій 1: override (динамічне зв'язування) ---");
Console.WriteLine($"Через змінну BaseNotifier:    {baseRef.SendOverride()}");
Console.WriteLine($"Через змінну DerivedNotifier: {derivedRef.SendOverride()}");

Console.WriteLine("\n--- Сценарій 2: new (приховування імені) ---");
Console.WriteLine($"Через змінну BaseNotifier:    {baseRef.SendNew()}");
Console.WriteLine($"Через змінну DerivedNotifier: {derivedRef.SendNew()}");

namespace Task5.OverrideVsNew
{
    public class BaseNotifier
    {
        public virtual string SendOverride() => "BaseNotifier.SendOverride()";
        public virtual string SendNew() => "BaseNotifier.SendNew()";
    }

    public class DerivedNotifier : BaseNotifier
    {
        public override string SendOverride() => "DerivedNotifier.SendOverride()";
        public new string SendNew() => "DerivedNotifier.SendNew()";
    }
}