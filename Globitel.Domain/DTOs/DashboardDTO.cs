namespace Globitel.Domain.DTO
{
    public class DashboardDTO
    {
        public GenderChartDTO GenderChart { get; set; }
        public AgeChartDTO AgeChart { get; set; }
    }
    public class AgeChartDTO
    {
        public int SmallClassCount { get; set; }
        public int MedClassCount { get; set; }
        public int OldClassCount { get; set; }
    } 
    public class GenderChartDTO
    {
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
    }
}
