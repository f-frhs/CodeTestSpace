// collectDataFroXmlFiles.cs <= mgetFileName.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

class Program
{
    private static List<Tuple<string, string, string, double>> list;

    static void Main(string[] args)
    {
        var CHY1_X = new List<double>();
        var CHY1_Y = new List<double>();
        var CHY1_Z = new List<double>();
        var CHY1_I = new List<double>();
        var CHY1_J = new List<double>();
        var CHY1_K = new List<double>();
        var CHY1_D = new List<double>();
        var FHY1_X = new List<double>();
        var FHY1_Y = new List<double>();
        var FHY1_Z = new List<double>();
        var FHY1_I = new List<double>();
        var FHY1_J = new List<double>();
        var FHY1_K = new List<double>();
        var FHY1_D = new List<double>();
        var CHY2_X = new List<double>();
        var CHY2_Y = new List<double>();
        var CHY2_Z = new List<double>();
        var CHY2_I = new List<double>();
        var CHY2_J = new List<double>();
        var CHY2_K = new List<double>();
        var CHY2_D = new List<double>();
        var FHY2_X = new List<double>();
        var FHY2_Y = new List<double>();
        var FHY2_Z = new List<double>();
        var FHY2_I = new List<double>();
        var FHY2_J = new List<double>();
        var FHY2_K = new List<double>();
        var FHY2_D = new List<double>();
        var CHY3_X = new List<double>();
        var CHY3_Y = new List<double>();
        var CHY3_Z = new List<double>();
        var CHY3_I = new List<double>();
        var CHY3_J = new List<double>();
        var CHY3_K = new List<double>();
        var CHY3_D = new List<double>();
        var FHY3_X = new List<double>();
        var FHY3_Y = new List<double>();
        var FHY3_Z = new List<double>();
        var FHY3_I = new List<double>();
        var FHY3_J = new List<double>();
        var FHY3_K = new List<double>();
        var FHY3_D = new List<double>();
        var HNY1_X = new List<double>();
        var HNY1_Y = new List<double>();
        var HNY1_Z = new List<double>();
        var HNY1_I = new List<double>();
        var HNY1_J = new List<double>();
        var HNY1_K = new List<double>();
        var HNY1_D = new List<double>();
        var HNY2_X = new List<double>();
        var HNY2_Y = new List<double>();
        var HNY2_Z = new List<double>();
        var HNY2_I = new List<double>();
        var HNY2_J = new List<double>();
        var HNY2_K = new List<double>();
        var HNY2_D = new List<double>();

        var CHN1_X = new List<double>();
        var CHN1_Y = new List<double>();
        var CHN1_Z = new List<double>();
        var CHN1_I = new List<double>();
        var CHN1_J = new List<double>();
        var CHN1_K = new List<double>();
        var CHN1_D = new List<double>();
        var FHN1_X = new List<double>();
        var FHN1_Y = new List<double>();
        var FHN1_Z = new List<double>();
        var FHN1_I = new List<double>();
        var FHN1_J = new List<double>();
        var FHN1_K = new List<double>();
        var FHN1_D = new List<double>();
        var CHN2_X = new List<double>();
        var CHN2_Y = new List<double>();
        var CHN2_Z = new List<double>();
        var CHN2_I = new List<double>();
        var CHN2_J = new List<double>();
        var CHN2_K = new List<double>();
        var CHN2_D = new List<double>();
        var FHN2_X = new List<double>();
        var FHN2_Y = new List<double>();
        var FHN2_Z = new List<double>();
        var FHN2_I = new List<double>();
        var FHN2_J = new List<double>();
        var FHN2_K = new List<double>();
        var FHN2_D = new List<double>();
        var CHN3_X = new List<double>();
        var CHN3_Y = new List<double>();
        var CHN3_Z = new List<double>();
        var CHN3_I = new List<double>();
        var CHN3_J = new List<double>();
        var CHN3_K = new List<double>();
        var CHN3_D = new List<double>();
        var FHN3_X = new List<double>();
        var FHN3_Y = new List<double>();
        var FHN3_Z = new List<double>();
        var FHN3_I = new List<double>();
        var FHN3_J = new List<double>();
        var FHN3_K = new List<double>();
        var FHN3_D = new List<double>();
        var HNN1_X = new List<double>();
        var HNN1_Y = new List<double>();
        var HNN1_Z = new List<double>();
        var HNN1_I = new List<double>();
        var HNN1_J = new List<double>();
        var HNN1_K = new List<double>();
        var HNN1_D = new List<double>();
        var HNN2_X = new List<double>();
        var HNN2_Y = new List<double>();
        var HNN2_Z = new List<double>();
        var HNN2_I = new List<double>();
        var HNN2_J = new List<double>();
        var HNN2_K = new List<double>();
        var HNN2_D = new List<double>();

        foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\csvtesrt"))
        {
            XmlRead(file);

            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "X")) { CHY1_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Y")) { CHY1_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Z")) { CHY1_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Orientation I")) { CHY1_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Orientation J")) { CHY1_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Orientation K")) { CHY1_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Diameter")) { CHY1_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "X")) { FHY1_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Y")) { FHY1_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Z")) { FHY1_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Orientation I")) { FHY1_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Orientation J")) { FHY1_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Orientation K")) { FHY1_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Diameter")) { FHY1_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "X")) { CHY2_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Y")) { CHY2_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Z")) { CHY2_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Orientation I")) { CHY2_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Orientation J")) { CHY2_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Orientation K")) { CHY2_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Diameter")) { CHY2_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "X")) { FHY2_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Y")) { FHY2_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Z")) { FHY2_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Orientation I")) { FHY2_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Orientation J")) { FHY2_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Orientation K")) { FHY2_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Diameter")) { FHY2_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "X")) { CHY3_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Y")) { CHY3_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Z")) { CHY3_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Orientation I")) { CHY3_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Orientation J")) { CHY3_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Orientation K")) { CHY3_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Diameter")) { CHY3_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "X")) { FHY3_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Y")) { FHY3_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Z")) { FHY3_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Orientation I")) { FHY3_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Orientation J")) { FHY3_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Orientation K")) { FHY3_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Diameter")) { FHY3_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "X")) { HNY1_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Y")) { HNY1_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Z")) { HNY1_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Orientation I")) { HNY1_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Orientation J")) { HNY1_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Orientation K")) { HNY1_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Diameter")) { HNY1_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "X")) { HNY2_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Y")) { HNY2_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Z")) { HNY2_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Orientation I")) { HNY2_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Orientation J")) { HNY2_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Orientation K")) { HNY2_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Diameter")) { HNY2_K.Add(l.Item4); };

            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "X")) { CHN1_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Y")) { CHN1_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Z")) { CHN1_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Orientation I")) { CHN1_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Orientation J")) { CHN1_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Orientation K")) { CHN1_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Diameter")) { CHN1_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "X")) { FHN1_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Y")) { FHN1_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Z")) { FHN1_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Orientation I")) { FHN1_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Orientation J")) { FHN1_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Orientation K")) { FHN1_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Diameter")) { FHN1_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "X")) { CHN2_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Y")) { CHN2_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Z")) { CHN2_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Orientation I")) { CHN2_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Orientation J")) { CHN2_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Orientation K")) { CHN2_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Diameter")) { CHN2_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "X")) { FHN2_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Y")) { FHN2_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Z")) { FHN2_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Orientation I")) { FHN2_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Orientation J")) { FHN2_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Orientation K")) { FHN2_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Diameter")) { FHN2_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "X")) { CHN3_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Y")) { CHN3_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Z")) { CHN3_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Orientation I")) { CHN3_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Orientation J")) { CHN3_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Orientation K")) { CHN3_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Diameter")) { CHN3_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "X")) { FHN3_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Y")) { FHN3_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Z")) { FHN3_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Orientation I")) { FHN3_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Orientation J")) { FHN3_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Orientation K")) { FHN3_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Diameter")) { FHN3_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "X")) { HNN1_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Y")) { HNN1_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Z")) { HNN1_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Orientation I")) { HNN1_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Orientation J")) { HNN1_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Orientation K")) { HNN1_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Diameter")) { HNN1_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "X")) { HNN2_X.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Y")) { HNN2_Y.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Z")) { HNN2_Z.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Orientation I")) { HNN2_I.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Orientation J")) { HNN2_J.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Orientation K")) { HNN2_K.Add(l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Diameter")) { HNN2_K.Add(l.Item4); };
        }

        Console.WriteLine("----------結果---------------");
        var arrayCH1_X = CHY1_X.ToArray();
        var arrayCH2_X = CHY2_X.ToArray();
        var resoltCH1_2 = arrayCH2_X.Zip(arrayCH1_X, (x, y) => x - y).ToArray();
        var resoletX = CalcSD(resoltCH1_2);


        Console.WriteLine(resoletX);

        Console.ReadLine();
    }

    //ToDo: list全体をエクセルに吐き出す

    //ToDo: Listから任意の要素を抜き出す部分の関数化

    // List<doubel>を受取り、その平均と標準偏差計算を返す
    private static dynamic CalcSD(double[] p_Values)
    {
        //平均
        Double l_Ave = p_Values.Average();

        //σの二乗×データ数
        Double l_calcSD = 0;
        foreach (Double f_Value in p_Values)
        {
            l_calcSD += (f_Value - l_Ave) * (f_Value - l_Ave);
        }

        //σを算出して返却
        var l_SD = Math.Sqrt(l_calcSD / (p_Values.Length - 1));

        return new
        {
            ave = l_Ave,
            sd = l_SD
        };
    }

    //xmlのパス受取り、Absoluteの値をListに書き出す
    private static void XmlRead(string file)
    {
        using (StreamReader reader = new StreamReader(file))
        {
            DirectoryInfo dirInfo = Directory.GetParent(file);

            XmlSerializer serializer = new XmlSerializer(typeof(dot));
            var value = (dot)serializer.Deserialize(reader);
            list = new List<Tuple<string, string, string, double>>();

            foreach (inspectionpoint IP in value.Inspectionpoint)
            {
                foreach (characteristic CH in IP.Characteristic)
                {
                    foreach (measurement ME in CH.Measurement)
                    {
                        list.Add(Tuple.Create(dirInfo.Name, IP.Name, CH.Defaultname, double.Parse(ME.Absolute)));
                    }
                }
            }
        }
    }

    //指定階層下のフォルダにあるxmlのパスを返す
    static IEnumerable<string> GetFiles(string path)
    {
        Queue<string> queue = new Queue<string>();
        queue.Enqueue(path);
        while (queue.Count > 0)
        {
            path = queue.Dequeue();
            try
            {
                foreach (string subDir in Directory.GetDirectories(path))
                {
                    queue.Enqueue(subDir);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            string[] files = null;
            try
            {
                files = Directory.GetFiles(path, "*.xml");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    yield return files[i];
                }
            }
        }
    }
}

//以下xmlのデシリアライズ
[XmlRootAttribute(Namespace = "", IsNullable = false)]
public class dot
{
    //[XmlElement("cell")]
    //public cell[] Cell { get; set; }

    //[XmlElement("parttype")]
    //public parttype[] Parttype { get; set; }

    [XmlElement("cycle")]
    public cycle[] Cycle { get; set; }

    public string STORED { get; set; }

    [XmlElement("inspectionpoint")]
    public inspectionpoint[] Inspectionpoint { get; set; }
}

//[XmlRoot(Namespace = "", IsNullable = false)]
//public class cell
//{
//    [XmlElement("name")]
//    public string Name { get; set; }
//}

//[XmlRoot(Namespace = "", IsNullable = false)]
//public class parttype
//{
//    [XmlElement("name")]
//    public string Name { get; set; }
//}

[XmlRoot(Namespace = "", IsNullable = false)]
public class cycle
{
    [XmlElement("date")]
    public date[] Date { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class date
{
    [XmlElement("month")]
    public string Month { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class inspectionpoint
{
    [XmlElement("name")]
    public string Name { get; set; }

    [XmlElement("characteristic")]
    public characteristic[] Characteristic { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class characteristic
{
    [XmlElement("defaultname")]
    public string Defaultname { get; set; }

    [XmlElement("measurement")]
    public measurement[] Measurement { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class measurement
{
    [XmlElement("absolute")]
    public string Absolute { get; set; }
}
