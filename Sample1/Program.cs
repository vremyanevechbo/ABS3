using System;
abstract class Figure
{
    public abstract double CalculateArea();
}
class Circle : Figure
{
    private double radius;

    public Circle(double radius)
    {
        this.radius = radius;
    }
    public override double CalculateArea()
    {
        return Math.PI * Math.Pow(radius, 2);
    }
}
class Rectangle : Figure
{
    private double width;
    private double height;
    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }
    public override double CalculateArea()
    {
        return width * height;
    }
}
class Triangle : Figure
{
    private double baseLength;
    private double height;
    public Triangle(double baseLength, double height)
    {
        this.baseLength = baseLength;
        this.height = height;
    }
    public override double CalculateArea()
    {
        return 0.5 * baseLength * height;
    }
}
class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Выберите фигуру для расчета площади:");
            Console.WriteLine("1. Круг");
            Console.WriteLine("2. Прямоугольник");
            Console.WriteLine("3. Треугольник");
            Console.WriteLine("0. Выход");
            string choice = Console.ReadLine();
            Figure figure = null;
            switch (choice)
            {
                case "1":
                    Console.Write("Введите радиус круга: ");
                    double radius = Convert.ToDouble(Console.ReadLine());
                    figure = new Circle(radius);
                    break;
                case "2":
                    Console.Write("Введите ширину прямоугольника: ");
                    double width = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите высоту прямоугольника: ");
                    double height = Convert.ToDouble(Console.ReadLine());
                    figure = new Rectangle(width, height);
                    break;
                case "3":
                    Console.Write("Введите основание треугольника: ");
                    double baseLength = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите высоту треугольника: ");
                    double triHeight = Convert.ToDouble(Console.ReadLine());
                    figure = new Triangle(baseLength, triHeight);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    continue;
            }
            if (figure != null)
            {
                Console.WriteLine($"Площадь: {figure.CalculateArea()}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}