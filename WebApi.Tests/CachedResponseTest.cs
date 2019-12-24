using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Moq;
using Nominatim.API.Models;
using WebApi.Models;
using Xunit;

namespace WebApi.Integration.Tests
{
    public class CachedResponseTest : BaseTest
    {
        private readonly CachedResponse _cache = new CachedResponse
        {
            Id = new ObjectId("5e01a3b5ce7e59107cd17953"),
            CreatedAt = new DateTime(2019, 12, 24, 12, 00, 00),
            SearchText = "karakol",
            Content = new[]
            {
                new GeocodeResponse
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
                }
            }
        };

        [Fact]
        public void Get_ReturnsCorrectCache()
        {
            //CachedResponseMock.Setup(x => x.Get(It.IsAny<string>())).Returns(_cache);
            var response = MockCachedResponseService.Get("karakol");

            Assert.Equal(_cache.SearchText, response.SearchText);
            Assert.Equal(_cache.Content, response.Content);
            Assert.Equal(_cache.Id, response.Id);

        }

        [Fact]
        public void Check_ReturnsCorrectActualCache()
        {
            var result = MockCachedResponseService.IsActualCache(_cache);
            Assert.True(result);
        }
        [Fact]
        public void Cache_CheckForCorrectCache()
        {
            var result = MockCachedResponseService.IsActualCache(_cache);
            Assert.True(result);
        }

       public GeocodeResponse[] InitializeMockGeocode()
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
            return geocodes.ToArray();
        }


    }
}
