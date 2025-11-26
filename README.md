# Smart-Home-Automation-System

A C# **console-based application** that simulates a smart home system.  
Users can add devices, remove them, turn them on/off, adjust settings, and view their status.

## Features
- Add smart devices  
- Remove devices  
- Turn devices ON/OFF  
- Adjust settings (brightness, temperature, camera angle, etc.)  
- View status of all devices  
- Supports multiple device types:
  - Light  
  - Thermostat  
  - Camera  
  - Door Lock  

---

## Project Structure

### **Program.cs**
- Handles user menu  
- Accepts user input  
- Calls `SmartHome` methods  
- Contains functions for:
  - AddDevice  
  - RemoveDevice  
  - TurnDeviceOn  
  - TurnDeviceOff  
  - AdjustDeviceSettings  
  - ViewDeviceStatus  

### **SmartHome.cs**
Manages the entire smart home:
- Stores list of devices  
- Add/remove devices  
- Turn devices ON/OFF  
- Look up devices by ID  
- Display device statuses  

### **SmartDevice.cs**
Abstract base class for all devices.  
Includes:
- DeviceId  
- Name  
- Brand  
- Status (On/Off)

Methods:
- TurnOn()  
- TurnOff()  
- AdjustSettings() (abstract)

### **Device Types**

#### **Light**
- Color  
- Wattage  
- Brightness (adjustable)

#### **Thermostat**
- Temperature control

#### **Camera**
- Angle (0–180)  
- Optional zoom lens  

#### **DoorLock**
- Lock/unlock feature  
- No adjustable settings  

### **DeviceFactory.cs**
Creates device objects based on user input:
- light  
- thermostat  
- camera  
- doorlock  

---

## How to Run

1. Clone the project:
   ```
   git clone <your-repository-link>
   ```
2. Open in **Visual Studio**, **Rider**, or any C# IDE.
3. Build and run.
4. Use the menu to control the smart home system.

---

## Example Menu

```
Smart Home Automation System
1. Add Device
2. Remove Device
3. Turn Device On
4. Turn Device Off
5. Adjust Device Settings
6. View Device Status
7. Exit
Select an option:
```

---

## Future Enhancements
- Save/load device state using JSON  
- Add scheduling (timers, routines)  
- GUI or web interface  
- Add more smart device types  

---

