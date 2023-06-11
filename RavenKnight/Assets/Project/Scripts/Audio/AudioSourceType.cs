
namespace Audio
{
    /// <summary>
    /// Типы источников звуков.
    /// </summary>
    public enum AudioSourceType 
    {
        /// <summary>
        /// Неопознано. Тип нужен для проверки,
        /// что значение типа выбло выставлено.
        /// </summary>
        unknown = 0,
        /// <summary>
        /// Нажатие на кнопку.
        /// </summary>
        buttonClick,
        /// <summary>
        /// Общая музыка.
        /// </summary>
        generalMusic,

    }
}