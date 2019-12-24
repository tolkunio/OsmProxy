using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Nominatim.API.Models;

namespace WebApi.Models
{
    public static class SeedData
    {
        public static List<GeocodeResponse> InitializeMockGeocode()
        {
            List<GeocodeResponse> geocodes = new List<GeocodeResponse>();
            geocodes.Add(new GeocodeResponse
            {
                PlaceID = 115285809,
                License = "Data © OpenStreetMap contributors, ODbL 1.0. https://osm.org/copyright",
                OSMType = "way",
                OSMID = 171375636,
                Latitude = 42.4916808,
                Longitude = 78.3901729,
                DisplayName = "Каракол, Иссык-Кульская область, Кыргызстан",
                ExtraTags = new Dictionary<string, string>
                {
                    {"place","city"},
                    {"capital","4" },
                    {"wikidata","Q194452"},
                    {"wikipedia","ru:Каракол"},
                    {"population","63400"}
                },
                AlternateNames = new Dictionary<string, string>
                {
                    {"name","Каракол" },
                    {"name:de","Karakol"},
                    {"name:en" , "Karakol"},
                    {"name:et" , "Karakol"},
                    {"name:fi" , "Karakul"},
                    {"name:fr" , "Karakol"},
                    {"name:ja" , "カラコル"},
                    {"name:ky" , "Кара-Кол"},
                    {"name:la" , "Melanochiropolis"},
                    {"name:ru" , "Каракол"},
                    {"int_name", "Karakol"},
                    {"old_name", "Пржевальск"}
                },
                Address = new AddressResult
                {
                    Country = "Кыргызстан",
                    CountryCode = "kg",
                    County = null,
                    HouseNumber = null,
                    Road = null,
                    State = "Иссык-Кульская область",
                    Town = null,
                    Pedestrian = null,
                    Neighborhood = null,
                    Hamlet = null,
                    Suburb = null,
                    Village = null,
                    City = "Каракол",
                    Region = null,
                    District = null,
                    Name = null
                },
                Class = "place",
                ClassType = "city",
                Importance = 0.427143021264043,
                IconURL = "https://nominatim.openstreetmap.org/images/mapicons/poi_place_city.p.20.png",
                GeoKML = null,
                GeoSVG = null,
                GeoText = null

            });
            return geocodes;
        }
    }
}
