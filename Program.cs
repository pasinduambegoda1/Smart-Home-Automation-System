// program class
using System;
using System.Collections.Generic;

namespace SmartHomeAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartHome smartHome = new SmartHome();
            bool running = true;

            while (running)
            {
                //Console.Clear();
                Console.WriteLine("Smart Home Automation System");
                Console.WriteLine("1. Add Device");
                Console.WriteLine("2. Remove Device");
                Console.WriteLine("3. Turn Device On");
                Console.WriteLine("4. Turn Device Off");
                Console.WriteLine("5. Adjust Device Settings");
                Console.WriteLine("6. View Device Status");
                Console.WriteLine("7. Exit");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddDevice(smartHome);
                        break;
                    case "2":
                        RemoveDevice(smartHome);
                        break;
                    case "3":
                        TurnDeviceOn(smartHome);
                        break;
                    case "4":
                        TurnDeviceOff(smartHome);
                        break;
                    case "5":
                        AdjustDeviceSettings(smartHome);
                        break;
                    case "6":
                        ViewDeviceStatus(smartHome);
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AddDevice(SmartHome smartHome)
        {
            //Console.Clear();
            Console.WriteLine("Add Device");
            Console.Write("Enter device type (light, thermostat, camera, doorlock): ");
            string type = Console.ReadLine();
            Console.Write("Enter device ID: ");
            string deviceId = Console.ReadLine();
            Console.Write("Enter device name: ");
            string name = Console.ReadLine();
            Console.Write("Enter device brand: ");
            string brand = Console.ReadLine();

            try
            {
                var device = DeviceFactory.CreateDevice(type, deviceId, name, brand);
                smartHome.AddDevice(device);
                Console.WriteLine($"{name} added successfully. Press any key to continue.");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Error: {e.Message}. Press any key to continue.");
            }

            Console.ReadKey();
        }

        static void RemoveDevice(SmartHome smartHome)
        {
            //Console.Clear();
            Console.WriteLine("Remove Device");
            Console.Write("Enter device ID: ");
            string deviceId = Console.ReadLine();
            smartHome.RemoveDevice(deviceId);
            Console.WriteLine("Device removed successfully. Press any key to continue.");
            Console.ReadKey();
        }

        static void TurnDeviceOn(SmartHome smartHome)
        {
            //Console.Clear();
            Console.WriteLine("Turn Device On");
            Console.Write("Enter device ID: ");
            string deviceId = Console.ReadLine();
            smartHome.TurnDeviceOn(deviceId);
            Console.WriteLine("Device turned on successfully. Press any key to continue.");
            Console.ReadKey();
        }

        static void TurnDeviceOff(SmartHome smartHome)
        {
            Console.Clear();
            Console.WriteLine("Turn Device Off");
            Console.Write("Enter device ID: ");
            string deviceId = Console.ReadLine();
            smartHome.TurnDeviceOff(deviceId);
            Console.WriteLine("Device turned off successfully. Press any key to continue.");
            Console.ReadKey();
        }

        static void AdjustDeviceSettings(SmartHome smartHome)
        {
            //Console.Clear();
            Console.WriteLine("Adjust Device Settings");
            Console.Write("Enter device ID: ");
            string deviceId = Console.ReadLine();
            var device = smartHome.GetDevice(deviceId);

            if (device != null)
            {
                Console.WriteLine($"Adjusting settings for {device.Name} ({device.GetType().Name})");
                device.AdjustSettings();
                Console.WriteLine("Device settings adjusted successfully. Press any key to continue.");
            }
            else
            {
                Console.WriteLine("Device not found. Press any key to continue.");
            }

            Console.ReadKey();
        }

        static void ViewDeviceStatus(SmartHome smartHome)
        {
            //Console.Clear();
            Console.WriteLine("View Device Status");
            smartHome.ViewDeviceStatus();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}


// Smarthome class

public class SmartHome
{
    private List<SmartDevice> devices;

    public SmartHome()
    {
        devices = new List<SmartDevice>();
    }

    public void AddDevice(SmartDevice device)
    {
        devices.Add(device);
        Console.WriteLine($"{device.Name} added to the smart home.");
    }

    public void RemoveDevice(string deviceId)
    {
        var device = devices.FirstOrDefault(d => d.DeviceId == deviceId);
        if (device != null)
        {
            devices.Remove(device);
            Console.WriteLine($"{device.Name} removed from the smart home.");
        }
    }

    public SmartDevice GetDevice(string deviceId)
    {
        return devices.FirstOrDefault(d => d.DeviceId == deviceId);
    }

    public void TurnDeviceOn(string deviceId)
    {
        var device = GetDevice(deviceId);
        device?.TurnOn();
    }

    public void TurnDeviceOff(string deviceId)
    {
        var device = GetDevice(deviceId);
        device?.TurnOff();
    }

    public void AdjustDeviceSettings(string deviceId, int value)
    {
        var device = GetDevice(deviceId);
        if (device != null)
        {
            device.AdjustSettings();
        }
    }

    public void ViewDeviceStatus()
    {
        foreach (var device in devices)
        {
            Console.WriteLine($"{device.Name}: {(device.Status ? "On" : "Off")}");
        }
    }
}


// smartdevices class
public abstract class SmartDevice
{
    public string DeviceId { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; } // true for on, false for off
    public string Brand { get; set; }

    public SmartDevice(string deviceId, string name, string brand)
    {
        DeviceId = deviceId;
        Name = name;
        Brand = brand;
        Status = false;
    }

    public virtual void TurnOn()
    {
        Status = true;
        Console.WriteLine($"{Name} is turned on.");
    }

    public virtual void TurnOff()
    {
        Status = false;
        Console.WriteLine($"{Name} is turned off.");
    }

    public abstract void AdjustSettings();
}


// light class
public class Light : SmartDevice
{
    public string Color { get; set; }
    public int Wattage { get; set; }
    public int Brightness { get; set; }

    public Light(string deviceId, string name, string brand, string color, int wattage) 
        : base(deviceId, name, brand)
    {
        Color = color;
        Wattage = wattage;
    }

    public override void AdjustSettings()
    {
        Console.WriteLine("Enter brightness level (0-100): ");
        if (int.TryParse(Console.ReadLine(), out int brightness) && brightness >= 0 && brightness <= 100)
        {
            Brightness = brightness;
            Console.WriteLine($"{Name} brightness set to {Brightness}%.");
        }
        else
        {
            Console.WriteLine("Invalid brightness level.");
        }
    }
}

// Doorlock class
public class DoorLock : SmartDevice
{
    public bool IsLocked { get; set; }

    public DoorLock(string deviceId, string name, string brand) 
        : base(deviceId, name, brand)
    {
        IsLocked = false;
    }

    public override void AdjustSettings()
    {
        Console.WriteLine("Door locks do not have adjustable settings.");
    }

    public void Lock()
    {
        IsLocked = true;
        Console.WriteLine($"{Name} is locked.");
    }

    public void Unlock()
    {
        IsLocked = false;
        Console.WriteLine($"{Name} is unlocked.");
    }
}


// light class
public class Camera : SmartDevice
{
    public int Angle { get; set; }
    public bool HasZoomLens { get; set; }

    public Camera(string deviceId, string name, string brand, bool hasZoomLens) 
        : base(deviceId, name, brand)
    {
        HasZoomLens = hasZoomLens;
    }

    public override void AdjustSettings()
    {
        Console.Write("Enter camera angle (0-180): ");
        if (int.TryParse(Console.ReadLine(), out int angle) && angle >= 0 && angle <= 180)
        {
            Angle = angle;
            Console.WriteLine($"{Name} angle set to {Angle}°.");
        }
        else
        {
            Console.WriteLine("Invalid angle.");
        }
    }
}

// thermostat class
public class Thermostat : SmartDevice
{
    public int Temperature { get; set; }

    public Thermostat(string deviceId, string name, string brand) 
        : base(deviceId, name, brand) { }

    public override void AdjustSettings()
    {
        Console.Write("Enter temperature (°C): ");
        if (int.TryParse(Console.ReadLine(), out int temperature))
        {
            Temperature = temperature;
            Console.WriteLine($"{Name} temperature set to {Temperature}°C.");
        }
        else
        {
            Console.WriteLine("Invalid temperature.");
        }
    }
}


// Device Factor class'
public static class DeviceFactory
{
    public static SmartDevice CreateDevice(string type, string deviceId, string name, string brand)
    {
        switch (type.ToLower())
        {
            case "light":
                Console.Write("Enter light color: ");
                string color = Console.ReadLine();
                Console.Write("Enter light wattage: ");
                if (int.TryParse(Console.ReadLine(), out int wattage))
                {
                    return new Light(deviceId, name, brand, color, wattage);
                }
                throw new ArgumentException("Invalid wattage");
            case "thermostat":
                return new Thermostat(deviceId, name, brand);
            case "camera":
                Console.Write("Does the camera have a zoom lens? (yes/no): ");
                bool hasZoomLens = Console.ReadLine().ToLower() == "yes";
                return new Camera(deviceId, name, brand, hasZoomLens);
            case "doorlock":
                return new DoorLock(deviceId, name, brand);
            default:
                throw new ArgumentException("Invalid device type");
        }
    }
}
