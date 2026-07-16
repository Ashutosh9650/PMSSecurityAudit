using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for GeoUtils
/// </summary>
public class GeoUtils
{
	public GeoUtils()
	{
		//
		// TODO: Add constructor logic here


		//
     
	}
    public class Point
    {
        public Double Y { get; set; }
        public Double X { get; set; }
        public Point(Double X, Double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
    public class GeoPoint
    {
        public Double lat { get; set; }
        public Double lng { get; set; }
        public GeoPoint()
        {
        }
    }
    public class GeoPointChecker
    {
        private string err = "";
        private bool checkResult = false;

        public GeoPointChecker(string UserLocation, string VillageGeoPoly)
        {

            VillageGeoPoly = VillageGeoPoly.Replace("jb", "lat");
            VillageGeoPoly = VillageGeoPoly.Replace("kb", "lng");

            VillageGeoPoly = VillageGeoPoly.Replace("lb", "lat");
            VillageGeoPoly = VillageGeoPoly.Replace("mb", "lng");

            string[] userLoc = UserLocation.Split(new char[] { ',' });

            Point userPoint = new Point(Convert.ToDouble(userLoc[0]), Convert.ToDouble(userLoc[1]));

            if (VillageGeoPoly != null && VillageGeoPoly != "[]" && VillageGeoPoly != "")
            {

                JavaScriptSerializer js = new JavaScriptSerializer();


                try
                {

                    List<GeoPoint> polyData = (List<GeoPoint>)js.Deserialize(VillageGeoPoly, typeof(List<GeoPoint>));
                    if (polyData.Count < 0)
                    {
                        int ab = polyData.Count();

                    }
                    List<Point> pointList = new List<Point>() { };
                    foreach (GeoPoint gPoint in polyData)
                    {
                        pointList.Add(new Point(gPoint.lat, gPoint.lng));
                    }
                    checkResult = GeoUtils.IsPointInPolygon(pointList, userPoint);


                }
                catch (Exception)
                {
                    string b = UserLocation;
                    String abc = VillageGeoPoly;
                }
            }
        }
        public bool isValid()
        {
            return checkResult;
        }

        public static double abcd(string UserLocation, string VillageGeoPoly)
        {


            double ab = 0;
            VillageGeoPoly = VillageGeoPoly.Replace("jb", "lat");
            VillageGeoPoly = VillageGeoPoly.Replace("kb", "lng");

            VillageGeoPoly = VillageGeoPoly.Replace("lb", "lat");
            VillageGeoPoly = VillageGeoPoly.Replace("mb", "lng");

            string[] userLoc = UserLocation.Split(new char[] { ',' });
            string[] vilage_loc = VillageGeoPoly.Split(new char[] { ',' });
            Point userPoint = new Point(Convert.ToDouble(userLoc[0]), Convert.ToDouble(userLoc[1]));
            if (VillageGeoPoly != null && VillageGeoPoly != "[]" && VillageGeoPoly != "")
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<GeoPoint> polyData = (List<GeoPoint>)js.Deserialize(VillageGeoPoly, typeof(List<GeoPoint>));
                List<Point> pointList = new List<Point>() { };
                foreach (GeoPoint gPoint in polyData)
                {
                    pointList.Add(new Point(gPoint.lat, gPoint.lng));
                }
                ab = GeoUtils.calclatedistance(pointList, userPoint);
            }
            return ab;
        }


        public static string abcdNew(string UserLocation, string VillageGeoPoly)
        {

            string LatLOG="";
            double ab = 0;
            VillageGeoPoly = VillageGeoPoly.Replace("jb", "lat");
            VillageGeoPoly = VillageGeoPoly.Replace("kb", "lng");

            VillageGeoPoly = VillageGeoPoly.Replace("lb", "lat");
            VillageGeoPoly = VillageGeoPoly.Replace("mb", "lng");

            string[] userLoc = UserLocation.Split(new char[] { ',' });
            string[] vilage_loc = VillageGeoPoly.Split(new char[] { ',' });
            Point userPoint = new Point(Convert.ToDouble(userLoc[0]), Convert.ToDouble(userLoc[1]));
            if (VillageGeoPoly != null && VillageGeoPoly != "[]" && VillageGeoPoly != "")
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<GeoPoint> polyData = (List<GeoPoint>)js.Deserialize(VillageGeoPoly, typeof(List<GeoPoint>));
                List<Point> pointList = new List<Point>() { };
                foreach (GeoPoint gPoint in polyData)
                {
                 
                    LatLOG = gPoint.lat + "," + gPoint.lng;
                    break;
                }
               
            }
            return LatLOG;
        }

    }
    
        public static bool IsPointInPolygon(List<Point> polygon, Point testPoint)
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                string Village_longitude = polygon[i].Y.ToString();


                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y || polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        public static double calclatedistance(List<Point> polygon, Point testPoint)
        {
            float result = 0;
            int j = polygon.Count() - 1;
            double[] dist = new double[polygon.Count()];
            for (int i = 0; i < polygon.Count(); i++)
            {

                double Village_longitude1 = Convert.ToDouble(polygon[j].Y.ToString());
                double Village_latitude = Convert.ToDouble(polygon[i].X.ToString());
                double test_Longitude = Convert.ToDouble(testPoint.Y.ToString());
                double test_latitude = Convert.ToDouble(testPoint.X.ToString());
                var sCoord = new GeoCoordinate(Village_latitude, Village_longitude1);
                var eCoord = new GeoCoordinate(test_latitude, test_Longitude);
                var distance_test = eCoord.GetDistanceTo(sCoord) / 1000;
                Double distance1 = Convert.ToDouble(distance_test.ToString());
                dist[i] = distance1;

                j = i;
            }

            var abc = dist.Min();
            return Convert.ToDouble(abc);
        }

    
}