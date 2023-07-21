﻿using static WildlifeMortalities.Data.Entities.Mortalities.CaribouMortality;
using WildlifeMortalities.Data.Entities.Rules.BagLimit;
using WildlifeMortalities.Data.Entities.Seasons;
using WildlifeMortalities.Data.Entities;
using WildlifeMortalities.Data.Enums;
using WildlifeMortalities.Data;
using Microsoft.EntityFrameworkCore;

namespace WildlifeMortalities.DataSeeder;

internal class BagLimitSeeder
{
    private readonly AppDbContext _context;
    private Dictionary<string, GameManagementArea>? _areas;
    private Dictionary<string, HuntingSeason>? _huntingSeasons;

    public BagLimitSeeder(AppDbContext context)
    {
        _context = context;
    }

    internal async Task AddAllBagLimitEntries()
    {
        //if (!context.BagLimitEntries.Any())
        //{
        _areas = await _context.GameManagementAreas.ToDictionaryAsync(x => x.Area, x => x);
        _huntingSeasons = await _context.Seasons
            .OfType<HuntingSeason>()
            .ToDictionaryAsync(x => x.FriendlyName, x => x);

        AddAllHuntingBagLimitEntries2023To2024(_context, _huntingSeasons["23/24"], _areas);

        _context.SaveChanges();
        Console.WriteLine("Added BagLimitEntries");
        //}
        //else
        //{
        //    Console.WriteLine("BagLimitEntries already exist");
        //}
    }

    private void AddAllHuntingBagLimitEntries2023To2024(
        AppDbContext context,
        HuntingSeason season,
        Dictionary<string, GameManagementArea> areas
    )
    {
        var moose = new HuntingBagLimitEntry(
            GetGameManagementAreasFromIntegerArray(
                new int[]
                {
                    104, 105, 112, 113, 114, 115, 117, 118, 119, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130,
                    131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149,
                    150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168,
                    169, 170, 171, 172, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215,
                    216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234,
                    235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253,
                    254, 255, 257, 260, 261, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277,
                    278, 279, 280, 281, 282, 283, 284, 285, 286, 287, 288, 289, 290, 291, 292, 293, 301, 302, 303,
                    304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 401, 402,
                    407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 420, 421, 422, 423, 424, 425,
                    426, 427, 428, 429, 430, 431, 432, 433, 434, 435, 436, 437, 438, 439, 440, 441, 442, 443, 447,
                    448, 449, 450, 452, 501, 502, 503, 504, 505, 506, 507, 508, 509, 510, 511, 512, 513, 514, 515,
                    516, 517, 518, 519, 520, 521, 525, 527, 528, 529, 530, 531, 532, 533, 534, 535, 536, 537, 538,
                    539, 540, 541, 542, 543, 544, 545, 546, 547, 548, 549, 550, 551, 701, 702, 703, 704, 705, 706,
                    707, 708, 709, 710, 711, 712, 713, 714, 715, 716, 717, 718, 719, 720, 721, 722, 723, 724, 725,
                    726, 727, 728, 729, 730, 731, 732, 733, 734, 735, 736, 801, 802, 803, 804, 805, 806, 807, 808,
                    809, 810, 811, 812, 813, 814, 815, 816, 817, 818, 819, 820, 821, 822, 823, 824, 825, 826, 827,
                    901, 902, 903, 904, 905, 906, 907, 908, 909, 910, 911, 1001, 1002, 1003, 1004, 1005, 1006, 1007,
                    1008, 1009, 1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 1019, 1020, 1021, 1022, 1023,
                    1024, 1025, 1026, 1027, 1028, 1029, 1030, 1031, 1032, 1101, 1102, 1103, 1104, 1105, 1106, 1107,
                    1108, 1109, 1110, 1111, 1112, 1113, 1114, 1115, 1116, 1117, 1118, 1119, 1120, 1121, 1122, 1123,
                    1124, 1125, 1126, 1127, 1128, 1129, 1130, 1131, 1132, 1133, 1134, 1135, 1136, 1137, 1138, 1139,
                    1140, 1141, 1142, 1143, 1144, 1145, 1146
                }
            ),
            Species.Moose,
            season,
            GetSeasonStart(season, 8, 1),
            GetSeasonEnd(season, 10, 31),
            1, Sex.Male
        );

        var mooseFaroThreshold = new HuntingBagLimitEntry(
            GetGameManagementAreasFromIntegerArray(
                new int[] { 444, 445, 446 }
            ),
            Species.Moose,
            season,
            GetSeasonStart(season, 8, 1),
            GetSeasonEnd(season, 10, 31),
            1, Sex.Male, 15
        );

        var mooseMayoThreshold = new HuntingBagLimitEntry(
            GetGameManagementAreasFromIntegerArray(
                new int[] { 256, 258, 259, 262, 263, 404, 405, 406 }
            ),
            Species.Moose,
            season,
            GetSeasonStart(season, 9, 1),
            GetSeasonEnd(season, 10, 31),
            1, Sex.Male, 11
        );

        moose.MaxValuePerPersonSharedWith.Add(mooseFaroThreshold);
        moose.MaxValuePerPersonSharedWith.Add(mooseMayoThreshold);
        mooseFaroThreshold.MaxValuePerPersonSharedWith.Add(moose);
        mooseFaroThreshold.MaxValuePerPersonSharedWith.Add(mooseMayoThreshold);
        mooseMayoThreshold.MaxValuePerPersonSharedWith.Add(moose);
        mooseMayoThreshold.MaxValuePerPersonSharedWith.Add(mooseFaroThreshold);

        context.AddRange(moose, mooseFaroThreshold, mooseMayoThreshold);

        var areasWithBagLimitEntry = moose.Areas.Concat(mooseFaroThreshold.Areas).Concat(mooseMayoThreshold.Areas);
        var duplicateAreas = areasWithBagLimitEntry.GroupBy(x => x).SelectMany(x => x.Skip(1));
        var missingAreas = areas.Values.Except(areasWithBagLimitEntry);

        var bison = new HuntingBagLimitEntry(
            GetGameManagementAreasFromIntegerArray(
                new int[]
                {
                    301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319,
                    320, 401, 402, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419,
                    420, 421, 422, 423, 424, 425, 426, 427, 428, 429, 430, 431, 432, 433, 434, 435, 436, 437, 438,
                    439, 440, 441, 442, 443, 444, 445, 446, 447, 448, 449, 450, 452, 501, 502, 503, 504, 505, 506,
                    507, 508, 509, 510, 511, 512, 513, 514, 515, 516, 517, 518, 519, 520, 521, 522, 523, 524, 525,
                    526, 527, 528, 529, 530, 531, 532, 533, 534, 535, 536, 537, 538, 539, 540, 541, 542, 543, 544,
                    545, 546, 547, 548, 549, 550, 551, 701, 702, 703, 704, 705, 706, 707, 708, 709, 710, 711, 712,
                    713, 714, 715, 716, 717, 718, 719, 720, 721, 722, 723, 724, 725, 726, 727, 728, 729, 730, 731,
                    732, 733, 734, 735, 736, 801, 802, 803, 804, 805, 806, 807, 808, 809, 810, 811, 812, 813, 814,
                    815, 816, 817, 818, 819, 820, 821, 822, 823, 824, 825, 826, 827, 901, 902, 903, 904, 905, 906,
                    907, 908, 909, 910, 911
                }),
            Species.WoodBison,
            season,
            GetSeasonStart(season, 9, 1),
            GetSeasonEnd(season, 3, 31),
            1);

        context.Add(bison);

        var coyote = new HuntingBagLimitEntry(GetGameManagementAreasFromIntegerArray(
                new int[]
                {
                    115, 117, 118, 119, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135,
                    136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154,
                    155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 201,
                    202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220,
                    221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
                    240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258,
                    259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277,
                    278, 279, 280, 281, 282, 283, 284, 285, 286, 287, 288, 289, 290, 291, 292, 293, 301, 302, 303,
                    304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 401, 402,
                    404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 420, 421, 422,
                    423, 424, 425, 426, 427, 428, 429, 430, 431, 432, 433, 434, 435, 436, 437, 438, 439, 440, 441,
                    442, 443, 444, 445, 446, 447, 448, 449, 450, 452, 501, 502, 503, 504, 505, 506, 507, 508, 509,
                    510, 511, 512, 513, 514, 515, 516, 517, 518, 519, 520, 521, 522, 523, 524, 525, 526, 527, 528,
                    529, 530, 531, 532, 533, 534, 535, 536, 537, 538, 539, 540, 541, 542, 543, 544, 545, 546, 547,
                    548, 549, 550, 551, 701, 702, 703, 704, 705, 706, 707, 708, 709, 710, 711, 712, 713, 714, 715,
                    716, 717, 718, 719, 720, 721, 722, 723, 724, 725, 726, 727, 728, 729, 730, 731, 732, 733, 734,
                    735, 736, 801, 802, 803, 804, 805, 806, 807, 808, 809, 810, 811, 812, 813, 814, 815, 816, 817,
                    818, 819, 820, 821, 822, 823, 824, 825, 826, 827, 901, 902, 903, 904, 905, 906, 907, 908, 909,
                    910, 911, 1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010, 1011, 1012, 1013, 1014,
                    1015, 1016, 1017, 1018, 1019, 1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 1030,
                    1031, 1032, 1101, 1102, 1103, 1104, 1105, 1106, 1107, 1108, 1109, 1110, 1111, 1112, 1113, 1114,
                    1115, 1116, 1117, 1118, 1119, 1120, 1121, 1122, 1123, 1124, 1125, 1126, 1127, 1128, 1129, 1130,
                    1131, 1132, 1133, 1134, 1135, 1136, 1137, 1138, 1139, 1140, 1141, 1142, 1143, 1144, 1145, 1146
                }),
            Species.Coyote,
            season,
            GetSeasonStart(season, 8, 1),
            GetSeasonEnd(season, 3, 31),
            BagLimitEntry.InfiniteMaxValuePerPerson);

        var caribou = new HuntingBagLimitEntry(
            GetGameManagementAreasFromIntegerArray(
                new int[]
                {
                    245, 252, 253, 254, 255, 256, 257, 258, 259, 265, 270, 271, 272, 273, 274, 275, 276, 277, 278,
                    279, 280, 281, 282, 283, 284, 285, 286, 287, 289, 290, 291, 292, 293, 401, 402, 404, 405, 406,
                    407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 420, 421, 422, 423, 424, 425,
                    426, 427, 428, 429, 430, 431, 432, 433, 434, 435, 436, 437, 438, 439, 440, 441, 442, 443, 444,
                    445, 446, 447, 448, 449, 450, 452, 511, 513, 522, 523, 524, 525, 526, 527, 529, 530, 531, 532,
                    533, 534, 535, 536, 537, 538, 540, 541, 542, 543, 544, 545, 546, 547, 548, 801, 802, 803, 804,
                    805, 806, 807, 808, 809, 810, 811, 818, 819, 820, 821, 822, 823, 824, 825, 1001, 1002, 1003,
                    1004, 1010, 1011, 1012, 1013, 1014, 1015, 1016, 1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027,
                    1028, 1029, 1030, 1031, 1032, 1101, 1119, 1124, 1125, 1126, 1127, 1128, 1129, 1130, 1131, 1132,
                    1133, 1134, 1135, 1136, 1137, 1138, 1139, 1140, 1141, 1142, 1143, 1144, 1145, 1146
                }), Species.Caribou, season, GetSeasonStart(season, 8, 1),
            GetSeasonEnd(season, 10, 31), 1, Sex.Male);

        var caribouPhaWithShortSeason = new HuntingBagLimitEntry(
            GetGameManagementAreasFromIntegerArray(
                new int[]
                {
                    1005, 1006, 1007, 1008, 1009, 1017, 1018, 1019, 1102, 1103, 1104, 1105, 1106, 1107, 1108, 1109,
                    1110, 1111, 1112, 1113, 1114, 1115, 1116, 1117, 1118, 1120, 1121, 1122, 1123
                }), Species.Caribou, season, GetSeasonStart(season, 8, 1),
            GetSeasonEnd(season, 9, 24), 1, Sex.Male);

        var caribouPorcupine = new HuntingBagLimitEntry(
            GetGameManagementAreasFromIntegerArray(
                new int[]
                {
                    104, 105, 112, 113, 114, 115, 117, 118, 119, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130,
                    131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149,
                    150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168,
                    169, 170, 171, 172, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215,
                    217, 218, 222, 226, 230, 231, 232, 233, 234, 235, 236, 237, 238, 242, 243, 244, 266, 267, 268,
                    269
                }), Species.Caribou, season, GetSeasonStart(season, 8, 1),
            GetSeasonEnd(season, 1, 31), 2, Sex.Male);

        var caribouPorcupineOverlapAreas = new HuntingBagLimitEntry(GetGameManagementAreasFromIntegerArray(
                new int[] { 216, 223, 227, 228, 239 }), Species.Caribou, season, GetSeasonStart(season, 11, 1),
            GetSeasonEnd(season, 1, 31), 2, Sex.Male);

        var caribouHartRiverOverlapAreas = new HuntingBagLimitEntry(GetGameManagementAreasFromIntegerArray(
                new int[] { 216, 223, 227, 228, 239 }), Species.Caribou, season, GetSeasonStart(season, 8, 1),
            GetSeasonEnd(season, 10, 31), 1, Sex.Male);

        var caribouFortymileSummerThreshold = new HuntingBagLimitEntry(GetGameManagementAreasFromIntegerArray(
                new int[] { 301, 302, 304 }), Species.Caribou, season, GetSeasonStart(season, 8, 1),
            GetSeasonEnd(season, 9, 9), 1, Sex.Male, 89);

        var caribouFortymileWinterThreshold = new HuntingBagLimitEntry(GetGameManagementAreasFromIntegerArray(
                new int[] { 219, 220, 221, 224, 301, 302, 303, 304, 306, 307, 310, 312 }), Species.Caribou, season,
            GetSeasonStart(season, 12, 1),
            GetSeasonEnd(season, 3, 31), 1, Sex.Male, 29);

        caribou.MaxValuePerPersonSharedWith.AddRange(new[]
        {
            caribouPhaWithShortSeason, caribouPorcupine, caribouPorcupineOverlapAreas, caribouHartRiverOverlapAreas,
            caribouFortymileSummerThreshold, caribouFortymileWinterThreshold
        });
        caribouPhaWithShortSeason.MaxValuePerPersonSharedWith.AddRange(new[]
        {
            caribou, caribouPorcupine, caribouPorcupineOverlapAreas, caribouHartRiverOverlapAreas,
            caribouFortymileSummerThreshold, caribouFortymileWinterThreshold
        });
        caribouPorcupine.MaxValuePerPersonSharedWith.AddRange(new[]
        {
            caribouPhaWithShortSeason, caribou, caribouPorcupineOverlapAreas, caribouHartRiverOverlapAreas,
            caribouFortymileSummerThreshold, caribouFortymileWinterThreshold
        });
        caribouPorcupineOverlapAreas.MaxValuePerPersonSharedWith.AddRange(new[]
        {
            caribouPhaWithShortSeason, caribouPorcupine, caribou, caribouHartRiverOverlapAreas,
            caribouFortymileSummerThreshold, caribouFortymileWinterThreshold
        });
        caribouHartRiverOverlapAreas.MaxValuePerPersonSharedWith.AddRange(new[]
        {
            caribouPhaWithShortSeason, caribouPorcupine, caribouPorcupineOverlapAreas, caribou,
            caribouFortymileSummerThreshold, caribouFortymileWinterThreshold
        });
        caribouFortymileSummerThreshold.MaxValuePerPersonSharedWith.AddRange(new[]
        {
            caribouPhaWithShortSeason, caribouPorcupine, caribouPorcupineOverlapAreas, caribouHartRiverOverlapAreas,
            caribou, caribouFortymileWinterThreshold
        });
        caribouFortymileWinterThreshold.MaxValuePerPersonSharedWith.AddRange(new[]
        {
            caribouPhaWithShortSeason, caribouPorcupine, caribouPorcupineOverlapAreas, caribouHartRiverOverlapAreas,
            caribouFortymileSummerThreshold, caribou
        });
    }

    private static DateTimeOffset GetSeasonStart(Season season, int month, int day)
    {
        var year = season switch
        {
            HuntingSeason => month >= 4 ? season.StartDate.Year : season.EndDate.Year,
            TrappingSeason => month >= 7 ? season.StartDate.Year : season.EndDate.Year,
            _ => throw new NotSupportedException()
        };
        return new DateTimeOffset(year, month, day, 0, 0, 0, TimeSpan.FromHours(-7));
    }

    private static DateTimeOffset GetSeasonEnd(Season season, int month, int day)
    {
        var year = season switch
        {
            HuntingSeason => month >= 4 ? season.StartDate.Year : season.EndDate.Year,
            TrappingSeason => month >= 7 ? season.StartDate.Year : season.EndDate.Year,
            _ => throw new NotSupportedException()
        };
        return new DateTimeOffset(year, month, day, 23, 59, 59, TimeSpan.FromHours(-7));
    }

    private IEnumerable<GameManagementArea> GetGameManagementAreasFromIntegerArray(
        int[] applicableAreas
    )
    {
        if (_areas is null) throw new Exception();
        var areas = new List<GameManagementArea>();
        foreach (var area in applicableAreas)
        {
            var areaString = area.ToString();
            areas.Add(_areas[areaString.Insert(areaString.Length - 2, "-")]);
        }

        return areas;
    }
}