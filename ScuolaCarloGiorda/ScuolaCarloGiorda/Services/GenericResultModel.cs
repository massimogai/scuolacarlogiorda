namespace ScuolaCarloGiorda.Services
{
    public class GenericResultModel<T>
    {
        public List<T> result { get; set; } = new();

        public int index { get; set; } = 0;
        public int pages { get; set; } = 0;

        public long total { get; set; } = 0;
    }
}