﻿using RoadWeather.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadWeather.Managers;
using RoadWeather.Models;
using Newtonsoft.Json;
using Moq;

namespace RoadWeather.Test.Fakes
{
    public class MockClock : IClock
    {
        private DateTime now;

        public MockClock(DateTime dt)
        {
            this.now = dt;
        }

        DateTime IClock.Now
        {
            get { return now; }
        }
    }

    public class MockLocationWeatherManager : ILocationWeatherManager
    {
        public async Task<Dictionary<LocationDetail, ForecastDailyEntry>> GetEntriesForLocationsLongTerm(List<LocationDetail> locations)
        {
            return null;
        }

        public async Task<Dictionary<LocationDetail, ForecastShortTermEntry>> GetEntriesForLocationsShortTerm(List<LocationDetail> locations)
        {
            return null;
        }
    }

    /// <summary>
    /// Returns mock data for location lat=49.2 lon=16.6 from 2014-12-06 22:00
    /// </summary>
    public class MockWeatherProvider : IWeatherProvider
    {

        public async Task<ForecastLongTerm> GetForecastLongTerm(Location location)
        {
            // request http://api.openweathermap.org/data/2.5/forecast/daily?lat=49.2&lon=16.6&APPID=3e498b17220c9b49140ea1bb8a94c010&units=metric&cnt=16
            // 2014-12-06 22:00

            string json = "{\"cod\":\"200\",\"message\":0.0032,\"city\":{\"id\":6639863,\"name\":\"Veveří\",\"coord\":{\"lon\":16.59794,\"lat\":49.202961},\"country\":\"CZ\",\"population\":0},\"cnt\":16,\"list\":[{\"dt\":1417860000,\"temp\":{\"day\":5.54,\"min\":4.85,\"max\":5.54,\"night\":4.85,\"eve\":5.54,\"morn\":5.54},\"pressure\":994.5,\"humidity\":99,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":1.35,\"deg\":351,\"clouds\":92,\"rain\":1},{\"dt\":1417946400,\"temp\":{\"day\":3.5,\"min\":2.48,\"max\":4.84,\"night\":2.48,\"eve\":3.97,\"morn\":3.54},\"pressure\":993.43,\"humidity\":98,\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04d\"}],\"speed\":4.19,\"deg\":30,\"clouds\":80},{\"dt\":1418032800,\"temp\":{\"day\":1.82,\"min\":-2.01,\"max\":3.34,\"night\":-2.01,\"eve\":2.65,\"morn\":1.32},\"pressure\":990.69,\"humidity\":97,\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04d\"}],\"speed\":2.41,\"deg\":275,\"clouds\":80},{\"dt\":1418119200,\"temp\":{\"day\":-0.88,\"min\":-4.33,\"max\":2.47,\"night\":-0.05,\"eve\":1.82,\"morn\":-3.6},\"pressure\":994.29,\"humidity\":92,\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01d\"}],\"speed\":1.36,\"deg\":328,\"clouds\":0},{\"dt\":1418205600,\"temp\":{\"day\":0.69,\"min\":-3.42,\"max\":1.48,\"night\":1.48,\"eve\":1.4,\"morn\":-3.42},\"pressure\":988.91,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":7.63,\"deg\":162,\"clouds\":82,\"rain\":0.3},{\"dt\":1418292000,\"temp\":{\"day\":4.09,\"min\":0.23,\"max\":4.09,\"night\":0.23,\"eve\":3.03,\"morn\":2.34},\"pressure\":984.35,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":4.08,\"deg\":178,\"clouds\":74,\"rain\":1.41},{\"dt\":1418378400,\"temp\":{\"day\":5.26,\"min\":2.13,\"max\":6.3,\"night\":3.88,\"eve\":6.3,\"morn\":2.13},\"pressure\":976.36,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":9.04,\"deg\":170,\"clouds\":36,\"rain\":1.88},{\"dt\":1418464800,\"temp\":{\"day\":7.35,\"min\":2.32,\"max\":7.35,\"night\":2.32,\"eve\":4.78,\"morn\":3.72},\"pressure\":986.23,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":3.17,\"deg\":242,\"clouds\":0,\"rain\":1.1},{\"dt\":1418551200,\"temp\":{\"day\":6.71,\"min\":-2.29,\"max\":6.71,\"night\":-2.29,\"eve\":0.67,\"morn\":0.96},\"pressure\":1002.06,\"humidity\":0,\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01d\"}],\"speed\":1.92,\"deg\":315,\"clouds\":31},{\"dt\":1418637600,\"temp\":{\"day\":1.7,\"min\":-0.81,\"max\":1.7,\"night\":1.29,\"eve\":0.35,\"morn\":-0.81},\"pressure\":988.81,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":2.9,\"deg\":214,\"clouds\":94,\"rain\":0.71},{\"dt\":1418724000,\"temp\":{\"day\":0.84,\"min\":-0.84,\"max\":0.84,\"night\":-0.84,\"eve\":-0.05,\"morn\":0.5},\"pressure\":982.19,\"humidity\":0,\"weather\":[{\"id\":601,\"main\":\"Snow\",\"description\":\"snow\",\"icon\":\"13d\"}],\"speed\":4.17,\"deg\":337,\"clouds\":100,\"rain\":1.2,\"snow\":4.01},{\"dt\":1418810400,\"temp\":{\"day\":1.75,\"min\":-4.03,\"max\":1.75,\"night\":-4.03,\"eve\":-2.65,\"morn\":-1.15},\"pressure\":992.44,\"humidity\":0,\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01d\"}],\"speed\":2.16,\"deg\":293,\"clouds\":60,\"snow\":0.03},{\"dt\":1418896800,\"temp\":{\"day\":-1.07,\"min\":-2.64,\"max\":-1.07,\"night\":-2.64,\"eve\":-2.01,\"morn\":-2.31},\"pressure\":991.14,\"humidity\":0,\"weather\":[{\"id\":601,\"main\":\"Snow\",\"description\":\"snow\",\"icon\":\"13d\"}],\"speed\":2.32,\"deg\":294,\"clouds\":99,\"snow\":8.67},{\"dt\":1418983200,\"temp\":{\"day\":-1.41,\"min\":-11.78,\"max\":-1.41,\"night\":-10,\"eve\":-11.78,\"morn\":-3.44},\"pressure\":992.66,\"humidity\":0,\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"light snow\",\"icon\":\"13d\"}],\"speed\":2.6,\"deg\":167,\"clouds\":38,\"snow\":1.3},{\"dt\":1419069600,\"temp\":{\"day\":-5.7,\"min\":-12.97,\"max\":-3.63,\"night\":-3.63,\"eve\":-12.97,\"morn\":-6.31},\"pressure\":997.5,\"humidity\":0,\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"light snow\",\"icon\":\"13d\"}],\"speed\":1.52,\"deg\":230,\"clouds\":15,\"snow\":0.18},{\"dt\":1419156000,\"temp\":{\"day\":-3.63,\"min\":-3.63,\"max\":-3.63,\"night\":-3.63,\"eve\":-3.63,\"morn\":-3.63},\"pressure\":990.67,\"humidity\":0,\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"light snow\",\"icon\":\"13d\"}],\"speed\":5.27,\"deg\":169,\"clouds\":100,\"snow\":0.08}]}";
            return JsonConvert.DeserializeObject<ForecastLongTerm>(json);
        }

        public async Task<ForecastShortTerm> GetForecastShortTerm(Location location)
        {
            // request http://api.openweathermap.org/data/2.5/forecast?lat=49.2&lon=16.6&APPID=3e498b17220c9b49140ea1bb8a94c010&units=metric
            // 2014-12-06 21:59

            string json = "{\"cod\":\"200\",\"message\":0.0045,\"city\":{\"id\":3070356,\"name\":\"Moravany\",\"coord\":{\"lon\":16.580259,\"lat\":49.1478},\"country\":\"CZ\",\"population\":0},\"cnt\":35,\"list\":[{\"dt\":1417888800,\"main\":{\"temp\":5.2,\"temp_min\":3.62,\"temp_max\":5.2,\"pressure\":993.12,\"sea_level\":1033.98,\"grnd_level\":993.12,\"humidity\":98,\"temp_kf\":1.58},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"overcast clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":88},\"wind\":{\"speed\":1.33,\"deg\":342.501},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-06 18:00:00\"},{\"dt\":1417899600,\"main\":{\"temp\":4.71,\"temp_min\":3.21,\"temp_max\":4.71,\"pressure\":993.22,\"sea_level\":1034.12,\"grnd_level\":993.22,\"humidity\":99,\"temp_kf\":1.5},\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10n\"}],\"clouds\":{\"all\":92},\"wind\":{\"speed\":1.5,\"deg\":347.003},\"rain\":{\"3h\":1},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-06 21:00:00\"},{\"dt\":1417910400,\"main\":{\"temp\":4.35,\"temp_min\":2.93,\"temp_max\":4.35,\"pressure\":992.79,\"sea_level\":1033.78,\"grnd_level\":992.79,\"humidity\":99,\"temp_kf\":1.42},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"overcast clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":92},\"wind\":{\"speed\":1.74,\"deg\":21.009},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-07 00:00:00\"},{\"dt\":1417921200,\"main\":{\"temp\":3.81,\"temp_min\":2.47,\"temp_max\":3.81,\"pressure\":992.17,\"sea_level\":1033.28,\"grnd_level\":992.17,\"humidity\":100,\"temp_kf\":1.34},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"overcast clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":92},\"wind\":{\"speed\":2.26,\"deg\":5.00378},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-07 03:00:00\"},{\"dt\":1417932000,\"main\":{\"temp\":3.38,\"temp_min\":2.12,\"temp_max\":3.38,\"pressure\":992.17,\"sea_level\":1033.17,\"grnd_level\":992.17,\"humidity\":98,\"temp_kf\":1.26},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":76},\"wind\":{\"speed\":2.78,\"deg\":6.50018},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-07 06:00:00\"},{\"dt\":1417942800,\"main\":{\"temp\":4.11,\"temp_min\":2.92,\"temp_max\":4.11,\"pressure\":992.21,\"sea_level\":1033.2,\"grnd_level\":992.21,\"humidity\":99,\"temp_kf\":1.18},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03d\"}],\"clouds\":{\"all\":44},\"wind\":{\"speed\":2.84,\"deg\":9.50018},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2014-12-07 09:00:00\"},{\"dt\":1417953600,\"main\":{\"temp\":5.42,\"temp_min\":4.31,\"temp_max\":5.42,\"pressure\":991.51,\"sea_level\":1032.38,\"grnd_level\":991.51,\"humidity\":99,\"temp_kf\":1.1},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04d\"}],\"clouds\":{\"all\":76},\"wind\":{\"speed\":2.82,\"deg\":9.50272},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2014-12-07 12:00:00\"},{\"dt\":1417964400,\"main\":{\"temp\":4.75,\"temp_min\":3.73,\"temp_max\":4.75,\"pressure\":990.97,\"sea_level\":1031.82,\"grnd_level\":990.97,\"humidity\":97,\"temp_kf\":1.03},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"overcast clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":92},\"wind\":{\"speed\":2.28,\"deg\":355.501},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-07 15:00:00\"},{\"dt\":1417975200,\"main\":{\"temp\":3.32,\"temp_min\":2.38,\"temp_max\":3.32,\"pressure\":990.76,\"sea_level\":1031.63,\"grnd_level\":990.76,\"humidity\":96,\"temp_kf\":0.95},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":64},\"wind\":{\"speed\":2.14,\"deg\":329.006},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-07 18:00:00\"},{\"dt\":1417986000,\"main\":{\"temp\":2.05,\"temp_min\":1.18,\"temp_max\":2.05,\"pressure\":990.58,\"sea_level\":1031.46,\"grnd_level\":990.58,\"humidity\":97,\"temp_kf\":0.87},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":56},\"wind\":{\"speed\":2.75,\"deg\":320.506},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-07 21:00:00\"},{\"dt\":1417996800,\"main\":{\"temp\":1.08,\"temp_min\":0.29,\"temp_max\":1.08,\"pressure\":990.02,\"sea_level\":1030.94,\"grnd_level\":990.02,\"humidity\":96,\"temp_kf\":0.79},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03n\"}],\"clouds\":{\"all\":48},\"wind\":{\"speed\":2.16,\"deg\":306.002},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-08 00:00:00\"},{\"dt\":1418007600,\"main\":{\"temp\":1.07,\"temp_min\":0.36,\"temp_max\":1.07,\"pressure\":989.31,\"sea_level\":1030.15,\"grnd_level\":989.31,\"humidity\":93,\"temp_kf\":0.71},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":76},\"wind\":{\"speed\":2.02,\"deg\":275.501},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-08 03:00:00\"},{\"dt\":1418018400,\"main\":{\"temp\":0.18,\"temp_min\":-0.45,\"temp_max\":0.18,\"pressure\":988.71,\"sea_level\":1029.71,\"grnd_level\":988.71,\"humidity\":91,\"temp_kf\":0.63},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03n\"}],\"clouds\":{\"all\":32},\"wind\":{\"speed\":2.87,\"deg\":285.505},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-08 06:00:00\"},{\"dt\":1418029200,\"main\":{\"temp\":1.7,\"temp_min\":1.15,\"temp_max\":1.7,\"pressure\":988.99,\"sea_level\":1029.9,\"grnd_level\":988.99,\"humidity\":95,\"temp_kf\":0.55},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04d\"}],\"clouds\":{\"all\":68},\"wind\":{\"speed\":3.21,\"deg\":286.002},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2014-12-08 09:00:00\"},{\"dt\":1418040000,\"main\":{\"temp\":3.79,\"temp_min\":3.32,\"temp_max\":3.79,\"pressure\":988.81,\"sea_level\":1029.35,\"grnd_level\":988.81,\"humidity\":96,\"temp_kf\":0.47},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04d\"}],\"clouds\":{\"all\":68},\"wind\":{\"speed\":4.16,\"deg\":306.503},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2014-12-08 12:00:00\"},{\"dt\":1418050800,\"main\":{\"temp\":2.58,\"temp_min\":2.19,\"temp_max\":2.58,\"pressure\":988.85,\"sea_level\":1029.52,\"grnd_level\":988.85,\"humidity\":95,\"temp_kf\":0.39},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":56},\"wind\":{\"speed\":3,\"deg\":308.502},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-08 15:00:00\"},{\"dt\":1418061600,\"main\":{\"temp\":0.46,\"temp_min\":0.14,\"temp_max\":0.46,\"pressure\":989.42,\"sea_level\":1030.14,\"grnd_level\":989.42,\"humidity\":92,\"temp_kf\":0.32},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03n\"}],\"clouds\":{\"all\":36},\"wind\":{\"speed\":1.82,\"deg\":284.504},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-08 18:00:00\"},{\"dt\":1418072400,\"main\":{\"temp\":-1.35,\"temp_min\":-1.58,\"temp_max\":-1.35,\"pressure\":989.52,\"sea_level\":1030.42,\"grnd_level\":989.52,\"humidity\":90,\"temp_kf\":0.24},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03n\"}],\"clouds\":{\"all\":32},\"wind\":{\"speed\":1.55,\"deg\":303.503},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-08 21:00:00\"},{\"dt\":1418083200,\"main\":{\"temp\":-2.92,\"temp_min\":-3.07,\"temp_max\":-2.92,\"pressure\":989.65,\"sea_level\":1030.59,\"grnd_level\":989.65,\"humidity\":84,\"temp_kf\":0.16},\"weather\":[{\"id\":801,\"main\":\"Clouds\",\"description\":\"few clouds\",\"icon\":\"02n\"}],\"clouds\":{\"all\":24},\"wind\":{\"speed\":1.32,\"deg\":302.51},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-09 00:00:00\"},{\"dt\":1418094000,\"main\":{\"temp\":-3.3,\"temp_min\":-3.38,\"temp_max\":-3.3,\"pressure\":989.81,\"sea_level\":1030.92,\"grnd_level\":989.81,\"humidity\":86,\"temp_kf\":0.08},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03n\"}],\"clouds\":{\"all\":44},\"wind\":{\"speed\":1.32,\"deg\":322.001},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-09 03:00:00\"},{\"dt\":1418104800,\"main\":{\"temp\":-3.83,\"temp_min\":-3.83,\"temp_max\":-3.83,\"pressure\":991.04,\"sea_level\":1032.21,\"grnd_level\":991.04,\"humidity\":86},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01n\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.31,\"deg\":335.002},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-09 06:00:00\"},{\"dt\":1418115600,\"main\":{\"temp\":-0.86,\"temp_min\":-0.86,\"temp_max\":-0.86,\"pressure\":992.83,\"sea_level\":1033.94,\"grnd_level\":992.83,\"humidity\":94},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01d\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.36,\"deg\":349.502},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2014-12-09 09:00:00\"},{\"dt\":1418126400,\"main\":{\"temp\":2.7,\"temp_min\":2.7,\"temp_max\":2.7,\"pressure\":993.33,\"sea_level\":1034.23,\"grnd_level\":993.33,\"humidity\":100},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01d\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.86,\"deg\":332.002},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2014-12-09 12:00:00\"},{\"dt\":1418137200,\"main\":{\"temp\":1.92,\"temp_min\":1.92,\"temp_max\":1.92,\"pressure\":995.36,\"sea_level\":1036.27,\"grnd_level\":995.36,\"humidity\":97},\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10n\"}],\"clouds\":{\"all\":88},\"wind\":{\"speed\":1.32,\"deg\":233.002},\"rain\":{\"3h\":2},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-09 15:00:00\"},{\"dt\":1418148000,\"main\":{\"temp\":0.62,\"temp_min\":0.62,\"temp_max\":0.62,\"pressure\":997.1,\"sea_level\":1038.4,\"grnd_level\":997.1,\"humidity\":95},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":56},\"wind\":{\"speed\":2.02,\"deg\":284.51},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-09 18:00:00\"},{\"dt\":1418158800,\"main\":{\"temp\":-1.23,\"temp_min\":-1.23,\"temp_max\":-1.23,\"pressure\":998.74,\"sea_level\":1039.96,\"grnd_level\":998.74,\"humidity\":89},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03n\"}],\"clouds\":{\"all\":32},\"wind\":{\"speed\":2.31,\"deg\":290.002},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-09 21:00:00\"},{\"dt\":1418169600,\"main\":{\"temp\":-2.62,\"temp_min\":-2.62,\"temp_max\":-2.62,\"pressure\":998.98,\"sea_level\":1040.46,\"grnd_level\":998.98,\"humidity\":86},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":68},\"wind\":{\"speed\":1.87,\"deg\":281.512},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-10 00:00:00\"},{\"dt\":1418180400,\"main\":{\"temp\":-1.32,\"temp_min\":-1.32,\"temp_max\":-1.32,\"pressure\":998.8,\"sea_level\":1040.27,\"grnd_level\":998.8,\"humidity\":89},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":56},\"wind\":{\"speed\":2.05,\"deg\":288},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-10 03:00:00\"},{\"dt\":1418191200,\"main\":{\"temp\":-0.79,\"temp_min\":-0.79,\"temp_max\":-0.79,\"pressure\":998.5,\"sea_level\":1040.01,\"grnd_level\":998.5,\"humidity\":91},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03n\"}],\"clouds\":{\"all\":48},\"wind\":{\"speed\":2.21,\"deg\":291.007},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-10 06:00:00\"},{\"dt\":1418202000,\"main\":{\"temp\":0.86,\"temp_min\":0.86,\"temp_max\":0.86,\"pressure\":998.12,\"sea_level\":1039.57,\"grnd_level\":998.12,\"humidity\":93},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01d\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.22,\"deg\":263.503},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2014-12-10 09:00:00\"},{\"dt\":1418212800,\"main\":{\"temp\":2.8,\"temp_min\":2.8,\"temp_max\":2.8,\"pressure\":995.91,\"sea_level\":1036.98,\"grnd_level\":995.91,\"humidity\":100},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01d\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.91,\"deg\":212.005},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2014-12-10 12:00:00\"},{\"dt\":1418223600,\"main\":{\"temp\":0.59,\"temp_min\":0.59,\"temp_max\":0.59,\"pressure\":994.06,\"sea_level\":1035.1,\"grnd_level\":994.06,\"humidity\":97},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03n\"}],\"clouds\":{\"all\":36},\"wind\":{\"speed\":2.02,\"deg\":178},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-10 15:00:00\"},{\"dt\":1418234400,\"main\":{\"temp\":-0.91,\"temp_min\":-0.91,\"temp_max\":-0.91,\"pressure\":993.16,\"sea_level\":1034.09,\"grnd_level\":993.16,\"humidity\":92},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":76},\"wind\":{\"speed\":3.01,\"deg\":181.005},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-10 18:00:00\"},{\"dt\":1418245200,\"main\":{\"temp\":0.05,\"temp_min\":0.05,\"temp_max\":0.05,\"pressure\":991.89,\"sea_level\":1032.85,\"grnd_level\":991.89,\"humidity\":96},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"overcast clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":88},\"wind\":{\"speed\":4.56,\"deg\":189.001},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-10 21:00:00\"},{\"dt\":1418256000,\"main\":{\"temp\":0.59,\"temp_min\":0.59,\"temp_max\":0.59,\"pressure\":991.61,\"sea_level\":1032.53,\"grnd_level\":991.61,\"humidity\":97},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"overcast clouds\",\"icon\":\"04n\"}],\"clouds\":{\"all\":88},\"wind\":{\"speed\":2.95,\"deg\":196.001},\"rain\":{\"3h\":0},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2014-12-11 00:00:00\"}]}";
            return JsonConvert.DeserializeObject<ForecastShortTerm>(json);
        }
    }

    public class MockWeatherUtils : IWeatherUtils
    {

        public bool AvailableForShortTermForecast(Trip trip)
        {
            throw new NotImplementedException();
        }

        public ForecastDailyEntry SelectLongTermEntry(LocationDetail location, ForecastLongTerm forecast)
        {
            var mockWeatherProvider = new MockWeatherProvider();
            var task = mockWeatherProvider.GetForecastLongTerm(new Location());
            Task.WhenAll(task);

            return task.Result.Entries[0];
        }

        public ForecastShortTermEntry SelectShortTermEntry(LocationDetail location, ForecastShortTerm forecast)
        {
            var mockWeatherProvider = new MockWeatherProvider();
            var task = mockWeatherProvider.GetForecastShortTerm(new Location());
            Task.WhenAll(task);

            return task.Result.Entries[0];
        }
    }


}
