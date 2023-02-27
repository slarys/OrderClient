using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using OrderClient.Data;
using OrderClient.DTO;
using OrderClient.Models.Orders;

namespace OrderClient.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderClientContext _context;

        public OrdersController(OrderClientContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            
            var orders = await _context.Order.Include(o => o.Client).ToListAsync();

            
            foreach (var order in orders)
            {
                var client = await _context.Client.FindAsync(order.ClientID); 
                order.Client.SecondName = client?.SecondName; 
            }


            return View(orders);
        }


        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Client)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        
        public IActionResult Create()
        {
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "ID");

            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Create(OrderCreatDto order)
        {
            
            if (ModelState.IsValid)
            {
                var newOrder = new Order
                {
                    OrderDate = order.OrderDate,
                    ClientID = order.ClientID,
                    Description = order.Description,
                    OrderPrice = order.OrderPrice,
                    CloseDate = order.CloseDate
                };

                var client = await _context.Client.FindAsync(order.ClientID);
                client.OrderAmount += 1;
                _context.Update(client);

                _context.Order.Add(newOrder);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }
        
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderEditDto = new OrderEditDto
            {
                OrderDate = order.OrderDate,
                ClientID = order.ClientID,
                Description = order.Description,
                OrderPrice = order.OrderPrice,
                CloseDate = order.CloseDate
            };

            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "ID", order.ClientID);

            return View(orderEditDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("ID,OrderDate,ClientID,Description,OrderPrice,CloseDate")] OrderEditDto orderDto)
        {
            if (id != orderDto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var order = await _context.Order.FindAsync(id);
                    if (order == null)
                    {
                        return NotFound();
                    }

                   
                    order.OrderDate = orderDto.OrderDate;
                    order.ClientID = orderDto.ClientID;
                    order.Description = orderDto.Description;
                    order.OrderPrice = orderDto.OrderPrice;
                    order.CloseDate = orderDto.CloseDate;

                    _context.Update(order);
                    await _context.SaveChangesAsync();

                    
                    var client = await _context.Client.FindAsync(orderDto.ClientID);
                    if (client != null)
                    {
                        client.OrderAmount += 1; 
                        _context.Update(client);
                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(orderDto.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "ID", orderDto.ClientID);
            return View(orderDto);
        }

       
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Client)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'OrderClientContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            
            var client = await _context.Client.FindAsync(order.ClientID);
            client.OrderAmount -= 1;
            _context.Update(client);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        
        
        private bool OrderExists(uint id)
        {
          return _context.Order.Any(e => e.ID == id);
        }
    }
}
