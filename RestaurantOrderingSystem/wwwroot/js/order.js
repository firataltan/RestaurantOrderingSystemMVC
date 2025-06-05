// Sipariş sepeti için JavaScript
class OrderCart {
    constructor() {
        this.items = [];
        this.tableId = this.getTableId();
        this.initializeEventListeners();
    }

    getTableId() {
        // Hidden input'tan table ID'yi al
        const tableIdInput = document.getElementById('current-table-id');
        return tableIdInput ? parseInt(tableIdInput.value) : null;
    }

    initializeEventListeners() {
        // Ürün ekleme butonları
        document.addEventListener('click', (e) => {
            if (e.target.closest('.add-to-cart')) {
                const button = e.target.closest('.add-to-cart');
                this.addToCart(button);
            }
        });

        // Sepeti temizle butonu
        document.getElementById('clear-cart')?.addEventListener('click', () => {
            this.clearCart();
        });

        // Sipariş gönder butonu
        document.getElementById('submit-order')?.addEventListener('click', () => {
            this.submitOrder();
        });

        // Miktar değiştirme butonları (dinamik olarak eklenen)
        document.addEventListener('click', (e) => {
            if (e.target.closest('.quantity-btn')) {
                const button = e.target.closest('.quantity-btn');
                const action = button.dataset.action;
                const itemId = parseInt(button.dataset.itemId);

                if (action === 'increase') {
                    this.updateQuantity(itemId, 1);
                } else if (action === 'decrease') {
                    this.updateQuantity(itemId, -1);
                }
            }
        });

        // Ürün silme butonları
        document.addEventListener('click', (e) => {
            if (e.target.closest('.remove-item')) {
                const button = e.target.closest('.remove-item');
                const itemId = parseInt(button.dataset.itemId);
                this.removeItem(itemId);
            }
        });
    }

    addToCart(button) {
        const itemId = parseInt(button.dataset.id);
        const itemName = button.dataset.name;
        const itemPrice = parseFloat(button.dataset.price);

        const existingItem = this.items.find(item => item.id === itemId);

        if (existingItem) {
            existingItem.quantity += 1;
        } else {
            this.items.push({
                id: itemId,
                name: itemName,
                price: itemPrice,
                quantity: 1
            });
        }

        this.updateCartDisplay();
        this.showToast(`${itemName} sepete eklendi!`, 'success');
    }

    updateQuantity(itemId, change) {
        const item = this.items.find(item => item.id === itemId);
        if (item) {
            item.quantity += change;
            if (item.quantity <= 0) {
                this.removeItem(itemId);
            } else {
                this.updateCartDisplay();
            }
        }
    }

    removeItem(itemId) {
        this.items = this.items.filter(item => item.id !== itemId);
        this.updateCartDisplay();
        this.showToast('Ürün sepetten kaldırıldı!', 'info');
    }

    clearCart() {
        this.items = [];
        this.updateCartDisplay();
        this.showToast('Sepet temizlendi!', 'info');
    }

    updateCartDisplay() {
        const cartItemsContainer = document.getElementById('cart-items');
        const cartTotal = document.getElementById('cart-total');
        const submitButton = document.getElementById('submit-order');

        if (this.items.length === 0) {
            cartItemsContainer.innerHTML = '<p class="text-muted text-center">Sepetiniz boş</p>';
            cartTotal.textContent = '0,00 ₺';
            submitButton.disabled = true;
            return;
        }

        let html = '';
        let total = 0;

        this.items.forEach(item => {
            const subtotal = item.price * item.quantity;
            total += subtotal;

            html += `
                <div class="d-flex justify-content-between align-items-center mb-2 p-2 border rounded">
                    <div class="flex-grow-1">
                        <h6 class="mb-1">${item.name}</h6>
                        <small class="text-muted">${item.price.toFixed(2)} ₺</small>
                    </div>
                    <div class="d-flex align-items-center">
                        <button class="btn btn-outline-secondary btn-sm quantity-btn" data-action="decrease" data-item-id="${item.id}">
                            <i class="bi bi-dash"></i>
                        </button>
                        <span class="mx-2 fw-bold">${item.quantity}</span>
                        <button class="btn btn-outline-secondary btn-sm quantity-btn" data-action="increase" data-item-id="${item.id}">
                            <i class="bi bi-plus"></i>
                        </button>
                        <button class="btn btn-outline-danger btn-sm ms-2 remove-item" data-item-id="${item.id}">
                            <i class="bi bi-trash"></i>
                        </button>
                    </div>
                </div>
                <div class="text-end mb-2">
                    <small class="text-success fw-bold">${subtotal.toFixed(2)} ₺</small>
                </div>
            `;
        });

        cartItemsContainer.innerHTML = html;
        cartTotal.textContent = `${total.toFixed(2)} ₺`;
        submitButton.disabled = false;
    }

    async submitOrder() {
        if (this.items.length === 0) {
            this.showToast('Sepetiniz boş!', 'warning');
            return;
        }

        if (!this.tableId) {
            this.showToast('Masa bilgisi bulunamadı!', 'error');
            return;
        }

        const submitButton = document.getElementById('submit-order');
        const originalText = submitButton.innerHTML;
        submitButton.disabled = true;
        submitButton.innerHTML = '<i class="bi bi-hourglass-split"></i> Gönderiliyor...';

        const orderData = {
            tableId: this.tableId,
            specialInstructions: document.getElementById('special-instructions').value || null,
            orderItems: this.items.map(item => ({
                menuItemId: item.id,
                quantity: item.quantity,
                specialInstructions: null
            }))
        };

        try {
            const response = await fetch('/Order/Create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
                },
                body: JSON.stringify(orderData)
            });

            if (response.ok) {
                const result = await response.json();
                this.showToast('Siparişiniz başarıyla gönderildi!', 'success');

                // Sepeti temizle
                this.clearCart();
                document.getElementById('special-instructions').value = '';

                // Onay sayfasına yönlendir
                setTimeout(() => {
                    window.location.href = `/Order/Confirmation/${result.orderId}`;
                }, 1500);
            } else {
                throw new Error('Sipariş gönderilemedi');
            }
        } catch (error) {
            console.error('Sipariş hatası:', error);
            this.showToast('Sipariş gönderilirken bir hata oluştu!', 'error');
        } finally {
            submitButton.disabled = false;
            submitButton.innerHTML = originalText;
        }
    }

    showToast(message, type = 'info') {
        // Bootstrap toast oluştur
        const toastContainer = this.getOrCreateToastContainer();
        const toastId = `toast-${Date.now()}`;

        const bgClass = {
            'success': 'bg-success',
            'error': 'bg-danger',
            'warning': 'bg-warning',
            'info': 'bg-info'
        }[type] || 'bg-info';

        const toast = document.createElement('div');
        toast.id = toastId;
        toast.className = `toast ${bgClass} text-white`;
        toast.setAttribute('role', 'alert');
        toast.innerHTML = `
            <div class="toast-body">
                ${message}
                <button type="button" class="btn-close btn-close-white float-end" data-bs-dismiss="toast"></button>
            </div>
        `;

        toastContainer.appendChild(toast);

        const bsToast = new bootstrap.Toast(toast);
        bsToast.show();

        // Toast otomatik kaldırılsın
        toast.addEventListener('hidden.bs.toast', () => {
            toast.remove();
        });
    }

    getOrCreateToastContainer() {
        let container = document.getElementById('toast-container');
        if (!container) {
            container = document.createElement('div');
            container.id = 'toast-container';
            container.className = 'position-fixed top-0 end-0 p-3';
            container.style.zIndex = '1055';
            document.body.appendChild(container);
        }
        return container;
    }
}

// Sayfa yüklendiğinde sipariş sepetini başlat
document.addEventListener('DOMContentLoaded', () => {
    if (document.getElementById('cart-items')) {
        window.orderCart = new OrderCart();
    }
});