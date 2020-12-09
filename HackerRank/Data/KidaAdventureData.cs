﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Data
{
    class KidaAdventureData
    {
        public static int[] data = new int[]
        {
            155, 126, 627, 781, 456, 196, 0, 81, 287, 911, 729, 471, 191, 216, 939, 776, 456, 562, 648, 844, 164, 991, 944, 529, 934, 819, 1, 872, 736, 597, 78, 729, 133, 638, 625, 286, 861, 144, 693, 901, 533, 155, 287, 115, 487, 790, 562, 548, 533, 669, 92, 196, 958, 650, 113, 246, 274, 22, 196, 896, 118, 148, 53, 92, 133, 256, 265, 447, 1, 155, 221, 64, 420, 911, 729, 912, 638, 451, 92, 438, 456, 803, 716, 872, 98, 718, 555, 456, 154, 27, 805, 456, 610, 386, 287, 7, 224, 846, 792, 78, 811, 555, 581, 852, 118, 452, 959, 1, 771, 356, 944, 346, 729, 527, 738, 610, 610, 896, 454, 100, 742, 988, 460, 386, 320, 116, 235, 556, 441, 927, 529, 314, 988, 967, 92, 818, 795, 796, 307, 456, 428, 838, 298, 615, 828, 634, 790, 959, 369, 92, 191, 664, 729, 298, 196, 532, 428, 807, 1, 212, 143, 155, 814, 364, 389, 1, 822, 1, 259, 993, 801, 287, 1, 78, 449, 484, 181, 56, 18, 555, 653, 386, 562, 764, 152, 451, 676, 463, 170, 106, 155, 314, 274, 689, 307, 525, 792, 539, 363, 911, 565, 683, 259, 538, 64, 833, 762, 81, 139, 83, 911, 956, 441, 189, 372, 562, 64, 883, 533, 456, 1, 818, 751, 538, 92, 1, 169, 911, 386, 729, 279, 224, 309, 997, 452, 538, 980, 312, 716, 921, 660, 716, 907, 227, 968, 386, 663, 792, 235, 870, 1, 477, 70, 524, 373, 729, 729, 933, 325, 476, 14, 350, 529, 435, 645, 174, 324, 1, 771, 839, 438, 78, 196, 707, 126, 806, 554, 13, 483, 165, 302, 169, 841, 772, 144, 647, 729, 100, 609, 749, 716, 1, 769, 161, 1, 338, 661, 727, 512, 551, 441, 155, 576, 729, 780, 664, 736, 92, 901, 445, 716, 686, 195, 978, 711, 489, 169, 323, 463, 289, 716, 763, 1, 862, 87, 256, 599, 259, 1, 27, 179, 181, 974, 333, 293, 806, 736, 147, 946, 1, 988, 603, 191, 573, 857, 516, 716, 848, 112, 22, 939, 554, 757, 14, 603, 889, 439, 984, 329, 974, 1, 431, 155, 113, 482, 53, 421, 324, 748, 769, 911, 245, 533, 456, 675, 8, 560, 386, 164, 883, 529, 456, 1, 532, 519, 627, 77, 81, 603, 924, 909, 482, 631, 653, 993, 792, 757, 555, 478, 715, 279, 296, 1, 971, 383, 242, 246, 696, 662, 155, 16, 779, 307, 911, 406, 221, 796, 271, 170, 484, 575, 272, 246, 737, 625, 25, 231, 718, 936, 957, 119, 855, 555, 287, 147, 716, 848, 27, 456, 737, 846, 346, 404, 1, 562, 174, 477, 629, 22, 716, 938, 260, 272, 727, 386, 482, 562, 861, 364, 715, 716, 848, 729, 638, 555, 1, 498, 857, 246, 1, 628, 757, 1, 289, 246, 364, 986, 329, 1, 986, 763, 100, 225, 327, 707, 287, 364, 727, 463, 750, 155, 470, 425, 379, 407, 716, 911, 1, 27, 125, 166, 309, 183, 868, 603, 896, 194, 485, 100, 155, 883, 16, 1, 545, 300, 456, 92, 924, 469, 562, 793, 998, 256, 1, 988, 828, 287, 272, 428, 550, 749, 1, 359, 828, 266, 627, 443, 386, 155, 381, 216, 1, 285, 555, 397, 896, 821, 824, 78, 763, 910, 256, 555, 456, 865, 732, 619, 729, 593, 603, 635, 874, 21, 92, 769, 727, 65, 763, 671, 785, 806, 672, 151, 737, 463, 16, 441, 714, 889, 544, 1, 256, 860, 560, 562, 9, 716, 1, 916, 53, 716, 911, 287, 155, 648, 126, 14, 538, 133, 514, 1, 376, 866, 667, 352, 602, 911, 759, 92, 729, 305, 763, 576, 81, 1, 914, 196, 988, 246, 456, 727, 784, 441, 595, 729, 274, 165, 538, 729, 600, 999, 82, 879, 386, 170, 771, 599, 529, 112, 988, 573, 452, 155, 501, 172, 962, 386, 92, 88, 500, 169, 155, 113, 456, 519, 936, 924, 792, 392, 421, 716, 848, 471, 9, 770, 889, 625, 716, 896, 879, 863, 1, 34, 653, 23, 321, 345, 372, 560, 369, 737, 188, 298, 33, 547, 755, 246, 378, 939, 456, 162, 806, 53, 529, 749, 732, 350, 270, 798, 980, 622, 581, 168, 13, 919, 939, 729, 848, 599, 652, 183, 265, 144, 846, 742, 575, 92, 56, 235, 763, 409, 133, 991, 337, 715, 408, 853, 582, 911, 911, 276, 331, 603, 807, 165, 193, 53, 520, 471, 144, 649, 302, 554, 304, 807, 991, 610, 456, 387, 206, 714, 831, 902, 372, 169, 146, 466, 996, 364, 56, 434, 309, 883, 979, 768, 265, 911, 445, 144, 333, 1, 991, 693, 64, 848, 599, 716, 191, 676, 575, 190, 848, 707, 155, 883, 911, 522, 784, 64, 419, 307, 736, 771, 231, 76, 638, 400, 599, 283, 562, 78, 946, 577, 147, 769, 829, 858, 576, 533, 158, 529, 597, 81, 155, 452, 289, 620, 23, 302, 943, 595, 599, 375, 518, 729, 865, 398, 753, 1, 911, 153, 793, 716, 911, 438, 220, 485, 92, 771, 716, 645, 562, 456, 247, 716, 477, 622, 302, 441, 576, 386, 92, 287, 883, 441, 114, 562, 951, 324, 364, 615, 264, 319, 429, 729, 749, 414, 576, 209, 581, 859, 295, 827, 21, 1, 140, 722, 911, 638, 133, 181, 144, 616, 486, 792, 456, 368, 716, 22, 295, 674, 78, 100, 604, 911, 332, 427, 769, 307, 298, 246, 342, 917, 529, 287, 660, 374, 896, 555, 638, 755, 471, 833, 361, 1, 1000, 991, 402, 907, 88, 573, 532, 34, 987, 637, 8, 13, 914, 729, 100, 170, 170, 1, 773, 302, 899, 391, 952, 595, 854, 169, 91, 1, 391, 256, 851, 769, 233, 727, 386, 809, 118, 368, 929, 246, 308, 638, 796, 666, 967, 883, 307, 429, 181, 531, 876, 638, 246, 15, 858, 439, 170, 471, 144, 869, 980, 529, 727, 638, 782, 595, 770, 246, 194, 438, 81, 757, 911, 92, 635, 925, 273, 100, 405, 870, 940, 498, 562, 988, 737
        };
    }
}
