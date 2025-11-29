namespace Internship_4_OOP.Infrastructure.Common;

public static class UserDistance
{
    public static double HevrsineDistance(decimal lat1, decimal lng1, decimal lat2, decimal lng2)
    { 
        const int earthRadius=6371;
        lat1 = ToRadians(lat1);
        lng1 = ToRadians(lng1);
        lat2 = ToRadians(lat2);
        lng2= ToRadians(lng2);

        var dlng = lng2 - lng1;
        var dlat = lat2 - lat1;

        var preAnswer = Math.Pow(Math.Sin((double)dlat / 2), 2) + Math.Cos((double)lat1) * Math.Cos((double)lat2) *
            Math.Pow(Math.Sin((double)dlng / 2), 2);

        var ans = 2 * Math.Asin(Math.Sqrt(preAnswer));
        return earthRadius * ans;

    }

    private static decimal ToRadians(decimal degrees)
    {
        return degrees*((decimal)Math.PI/180);
    }
}