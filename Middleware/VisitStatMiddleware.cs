using MusicStoreApi.Services;

namespace MusicStoreApi.Middleware
{
    public class VisitStatMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public VisitStatMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Проверяем, что это запрос на главную страницу
            if (context.Request.Path == "/api/product" && context.Request.Method == HttpMethod.Get.Method)
            {
                using (var scope = _serviceScopeFactory.CreateScope())  // Создаем новый scope
                {
                    var visitStatService = scope.ServiceProvider.GetRequiredService<IVisitStatService>();  // Разрешаем scoped сервис

                    // Проверяем, был ли уже запрос с cookie для учета посещений
                    var isFirstVisit = context.Request.Cookies["visited"] == null;

                    if (isFirstVisit)
                    {
                        // Инкрементируем посещения
                        await visitStatService.IncrementCurrentMonthVisitsAsync();

                        // Сохраняем cookie, чтобы пометить, что пользователь посетил сайт
                        context.Response.Cookies.Append("visited", "true", new CookieOptions { Expires = DateTime.Now.AddYears(1) });
                    }
                }
            }

            await _next(context);
        }
    }

}
