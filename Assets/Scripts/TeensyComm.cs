using UnityEngine;
using System.Collections.Generic;
using System.IO.Ports;
using System;
using UnityEngine.UI;

public class TeensyComm : MonoBehaviour
{
    private const string GIVE_INPUT = "a";  // signal to the Arduino that we want to receive data
    // public string COMMAND = "b";

    public string port = "COM7";
    public int baudrate = 115200;           // set to maximum baudrate

    public int n = 6; // every nth frame, do a data transfer on the serial port (n can be 1)
    public int timeout = 1000; // sets the serial timeout value before reporting error

    public List<int> values = new List<int>(); // our list of received values
    // public string commandToSend = "";           // to send commands to the Arduino

    //Setup parameters to connect to the serial port
    private static SerialPort serial;
    //public static string portStatus = "";  // can be used for debugging or status
    private static string incoming;         // the string coming from the serial port
    private static int counter = 0;         // framecounter needed for calculating when to send a 'command'

    // List of all com ports available on the system
    // public static List<string> comPorts = new List<string>();
    public static bool isPortActive = false;    // true = a comm port is opened
    public TeensyWorker valueSend;

    void Start()
    {
        CreateAndOpenConnection(port);
    }


    void Update()
    {
        if (isPortActive)
        {
            if (counter % n == 0)
            {
                // if (commandToSend != "")
                // {
                //     // send command to Arduino
                //     serial.Write(commandToSend);
                //     commandToSend = "";	// reset command to send
                // }
                // else
                // {
                    // signal that we want to receive values
                    serial.Write(GIVE_INPUT);
                    try
                    {
                        incoming = serial.ReadLine();
                        ParseLine(incoming);
                        // Debug.Log(incoming);
                    }
                    catch (TimeoutException)
                    {
                        Debug.Log("it failed");
                    }
                // }
            }
            counter++;
        }
    }


    void ParseLine(string strIn)
    {
        string[] svalues = strIn.Split(',');
        string padVal = "";
        List<int> list = new List<int>();
        for (int i = 0; i < 2; i++)
        {
            int numVal = Convert.ToInt32(svalues[i]);
            list.Add(numVal);
        }
        for (int i = 2; i < 4; i++)
        {
            int numVal = Convert.ToInt32(svalues[i]);
            if (numVal >= 3000)
            {
                padVal += "1";
            }
            else
            {
                padVal += "0";
            }
        }
        list.Add(Convert.ToInt32(padVal,2));
        list.Add(Convert.ToInt32(svalues[4],2));
        values = list;
        valueSend.GiveValues(values);
    }

    /// <summary>
    /// This function creates a serial port connection for the given commPortName
    /// Will be called from another script
    /// </summary>
    /// <param name="commPortName"></param>
    public void CreateAndOpenConnection(string commPortName)
    {
        // Gidi: before we create a SerialPort, first check if it already was created before
        if (serial != null)
        {
            if (serial.IsOpen)
            {
                serial.Close();
                isPortActive = false;
            }
        }
        // create the serial port
        serial = new SerialPort(commPortName, baudrate, Parity.None, 8, StopBits.One);
        // now try to open the connection
        OpenConnection();
    }
    public void OpenConnection()
    {
        try
        {
            if (serial != null)
            {
                if (serial.IsOpen)
                {
                    serial.Close();
                    print("Closing port, because it was already open!");
                    isPortActive = false;
                }
                else
                {
                    serial.Open();
                    serial.ReadTimeout = timeout;
                    FindObjectOfType<PlayerMovement>().teensy = true;
                    //print("Port Opened!");
                    isPortActive = true;
                }
            }
            else
            {
                print("Port == null");
            }
        }
        catch
        {
            FindObjectOfType<PlayerMovement>().teensy = false;
            print("port does not exist");
        }
    }


    void OnApplicationQuit()
    {
        if (serial != null)
        {
            serial.Close();
        }
    }
}