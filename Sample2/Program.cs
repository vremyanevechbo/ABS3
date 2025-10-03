using System;
using System.Threading;
using System.Threading.Tasks;
public class Notification
{
    // Определение событий для разных типов уведомлений
    public event EventHandler<MessageEventArgs> MessageSent;
    public event EventHandler<CallEventArgs> CallReceived;
    public event EventHandler<EmailEventArgs> EmailSent;
    // Метод для отправки сообщения
    public void SendMessage(string message)
    {
        OnMessageSent(new MessageEventArgs(message));
    }
    // Метод для получения звонка
    public void ReceiveCall(string caller)
    {
        OnCallReceived(new CallEventArgs(caller));
    }
    // Метод для отправки электронного письма
    public void SendEmail(string email)
    {
        OnEmailSent(new EmailEventArgs(email));
    }
    // Вызов события при отправке сообщения
    protected virtual void OnMessageSent(MessageEventArgs e)
    {
        MessageSent?.Invoke(this, e);
    }
    // Вызов события при получении звонка
    protected virtual void OnCallReceived(CallEventArgs e)
    {
        CallReceived?.Invoke(this, e);
    }
    // Вызов события при отправке электронного письма
    protected virtual void OnEmailSent(EmailEventArgs e)
    {
        EmailSent?.Invoke(this, e);
    }
    // Метод для рандомной отправки уведомлений
    public async Task StartSendingNotifications(CancellationToken cancellationToken)
    {
        Random random = new Random();
        string[] callSources = { "Иван", "Мария", "Алексей" };
        string[] emailMessages = { "Важное сообщение!", "Письмо от test@example.com", "Счет за услуги", "Промо-акция!" };
        string[] telegramMessages = { "Сообщение в Telegram", "Новое уведомление!", "Вам написали в Telegram" };
        string[] discordCalls = { "Звонок в Discord от Ивана", "Звонок в Discord от Марии", "Звонок в Discord от Алексея" };
        while (!cancellationToken.IsCancellationRequested)
        {
            // Рандомно выбираем тип уведомления
            int notificationType = random.Next(4);
            switch (notificationType)
            {
                case 0: // Звонок в Discord
                    string discordCaller = discordCalls[random.Next(discordCalls.Length)];
                    ReceiveCall(discordCaller);
                    SendMessage("Вы получили сообщение после звонка в Discord.");
                    break;
                case 1: // Электронное письмо
                    string emailContent = emailMessages[random.Next(emailMessages.Length)];
                    SendEmail(emailContent);
                    SendMessage("Вы получили сообщение после письма.");
                    break;
                case 2: // Сообщение в Telegram
                    string telegramContent = telegramMessages[random.Next(telegramMessages.Length)];
                    SendMessage(telegramContent);
                    SendEmail("Электронное письмо отправлено после сообщения в Telegram.");
                    break;
                case 3: // Звонок по телефону
                    string caller = callSources[random.Next(callSources.Length)];
                    ReceiveCall(caller);
                    SendMessage("Вы получили сообщение после звонка.");
                    break;
            }
            // Пустая строка для читаемости
            Console.WriteLine();
            // Задержка между блоками уведомлений
            await Task.Delay(random.Next(1000, 10000), cancellationToken);
        }
    }
}
// Классы для аргументов событий
public class MessageEventArgs : EventArgs
{
    public string Message { get; }
    public MessageEventArgs(string message)
    {
        Message = message;
    }
}
public class CallEventArgs : EventArgs
{
    public string Caller { get; }
    public CallEventArgs(string caller)
    {
        Caller = caller;
    }
}
public class EmailEventArgs : EventArgs
{
    public string Email { get; }
    public EmailEventArgs(string email)
    {
        Email = email;
    }
}
// Класс для тестирования системы уведомлений
class Program
{
    static async Task Main()
    {
        Notification notification = new Notification();
        // Регистрация обработчиков событий
        notification.MessageSent += (sender, e) =>
            Console.WriteLine($"Сообщение отправлено: {e.Message}");
        notification.CallReceived += (sender, e) =>
            Console.WriteLine($"Получен звонок от: {e.Caller}");
        notification.EmailSent += (sender, e) =>
            Console.WriteLine($"Электронное письмо отправлено: {e.Email}");
        // Создание токена отмены
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            // Запуск отправки уведомлений
            Task notificationTask = notification.StartSendingNotifications(cts.Token);
            Console.WriteLine("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
            // Отмена отправки уведомлений
            cts.Cancel();
            await notificationTask; // Ожидание завершения задачи
        }
    }
}