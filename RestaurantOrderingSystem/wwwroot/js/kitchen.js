// Mutfak ekranı için JavaScript
class KitchenManager {
    constructor() {
        this.initializeEventListeners();
        this.startAutoRefresh();
    }

    initializeEventListeners() {
        // Sipariş durumu güncelleme butonları
        document.addEventListener('click', (e) => {
            if (e.target.closest('.update-status')) {
                const button = e.target.closest('.update-status');
                const orderId = parseInt(button.dataset.orderId);
                const status = parseInt(button.dataset.status);
                this.updateOrderStatus(orderId, status, button);
            }
        });

        // Manuel yenileme butonu
        document.getElementById('refresh-orders')?.addEventListener('click', () => {
            this.refreshOrders();
        });
    }

    async updateOrderStatus(orderId, status, button) {
        const originalText = button.innerHTML;
        button.disabled = true;
        button.innerHTML = '<i class="bi bi-hourglass-split"></i> Güncelleniyor...';

        try {
            const response = await fetch('/Kitchen/UpdateStatus', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
                },
                body: JSON.stringify({
                    orderId: orderId,
                    status: status
                })
            });

            if (response.ok) {
                const result = await response.json();
                this.showToast(result.message, 'success');

                // Sipariş kartını güncelle
                this.updateOrderCard(orderId, status);

                // Eğer sipariş tamamlandıysa kartı kaldır
                if (status === 4) { // Served
                    setTimeout(() => {
                        this.removeOrderCard(orderId);
                    }, 2000);
                }
            } else {
                throw new Error('Durum güncellenemedi');
            }
        } catch (error) {
            console.error('Durum güncelleme hatası:', error);
            this.showToast('Durum güncellenirken bir hata oluştu!', 'error');
        } finally {
            button.disabled = false;
            button.innerHTML = originalText;
        }
    }

    updateOrderCard(orderId, newStatus) {
        const orderCard = document.querySelector(`[data-order-id="${orderId}"]`);
        if (!orderCard) return;

        // Border rengini güncelle
        const card = orderCard.querySelector('.card');
        card.className = card.className.replace(/border-(danger|warning|success)/g, '');

        let borderClass = 'border-success';
        let badgeClass = 'bg-success';
        let statusText = 'Tamamlandı';

        if (newStatus === 2) { // Preparing
            borderClass = 'border-warning';
            badgeClass = 'bg-warning';
            statusText = 'Hazırlanıyor';
        } else if (newStatus === 3) { // Ready
            borderClass = 'border-success';
            badgeClass = 'bg-success';
            statusText = 'Hazır';
        } else if (newStatus === 4) { // Served
            borderClass = 'border-info';
            badgeClass = 'bg-info';
            statusText = 'Servis Edildi';
        }

        card.classList.add(borderClass);

        // Badge'i güncelle
        const badge = orderCard.querySelector('.badge');
        if (badge) {
            badge.className = `badge ${badgeClass}`;
            badge.textContent = statusText;
        }

        // Butonları güncelle
        const cardFooter = orderCard.querySelector('.card-footer');
        let buttonHtml = '';

        if (newStatus === 2) { // Preparing -> Ready butonunu göster
            buttonHtml = `
                <button class="btn btn-success btn-sm update-status" data-order-id="${orderId}" data-status="3">
                    <i class="bi bi-check"></i> Hazır
                </button>
            `;
        } else if (newStatus === 3) { // Ready -> Served butonunu göster
            buttonHtml = `
                <button class="btn btn-info btn-sm update-status" data-order-id="${orderId}" data-status="4">
                    <i class="bi bi-truck"></i> Servis Edildi
                </button>
            `;
        }

        // Detay butonunu her zaman göster
        buttonHtml += `
            <a href="/Kitchen/OrderDetails/${orderId}" class="btn btn-outline-primary btn-sm">
                <i class="bi bi-eye"></i> Detay
            </a>
        `;

        cardFooter.innerHTML = buttonHtml;
    }

    removeOrderCard(orderId) {
        const orderCard = document.querySelector(`[data-order-id="${orderId}"]`);
        if (orderCard) {
            orderCard.style.transition = 'opacity 0.5s';
            orderCard.style.opacity = '0';
            setTimeout(() => {
                orderCard.remove();
                this.checkIfNoOrders();
            }, 500);
        }
    }

    checkIfNoOrders() {
        const ordersContainer = document.getElementById('orders-container');
        const orderCards = ordersContainer.querySelectorAll('.order-card');

        if (orderCards.length === 0) {
            ordersContainer.innerHTML = `
                <div class="col-12">
                    <div class="alert alert-info text-center" role="alert">
                        <i class="bi bi-info-circle display-4"></i>
                        <h4 class="mt-3">Aktif sipariş bulunmuyor</h4>
                        <p>Yeni siparişler geldiğinde burada görünecektir.</p>
                    </div>
                </div>
            `;
        }
    }

    async refreshOrders() {
        const refreshButton = document.getElementById('refresh-orders');
        const originalText = refreshButton.innerHTML;
        refreshButton.disabled = true;
        refreshButton.innerHTML = '<i class="bi bi-hourglass-split"></i> Yenileniyor...';

        try {
            // Sayfayı yenile
            window.location.reload();
        } catch (error) {
            console.error('Yenileme hatası:', error);
            this.showToast('Sayfa yenilenirken bir hata oluştu!', 'error');
        } finally {
            refreshButton.disabled = false;
            refreshButton.innerHTML = originalText;
        }
    }

    startAutoRefresh() {
        // Her 30 saniyede bir yeni siparişleri kontrol et
        setInterval(() => {
            this.checkForNewOrders();
        }, 30000);
    }

    async checkForNewOrders() {
        try {
            const response = await fetch('/Kitchen/GetPendingOrders');
            if (response.ok) {
                const orders = await response.json();
                this.updateOrdersDisplay(orders);
            }
        } catch (error) {
            console.error('Yeni sipariş kontrolü hatası:', error);
        }
    }

    updateOrdersDisplay(orders) {
        // Mevcut sipariş sayısını kontrol et
        const currentOrderCards = document.querySelectorAll('.order-card');
        const currentOrderIds = Array.from(currentOrderCards).map(card =>
            parseInt(card.dataset.orderId)
        );

        // Yeni siparişleri kontrol et
        const newOrders = orders.filter(order => !currentOrderIds.includes(order.id));

        if (newOrders.length > 0) {
            this.showToast(`${newOrders.length} yeni sipariş geldi!`, 'info');
            // Sayfa yenilemesi yerine dinamik olarak yeni siparişleri ekleyebiliriz
            // Şimdilik sayfa yenileme kullanıyoruz
            setTimeout(() => {
                window.location.reload();
            }, 1000);
        }
    }

    showToast(message, type = 'info') {
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

// Sayfa yüklendiğinde mutfak yöneticisini başlat
document.addEventListener('DOMContentLoaded', () => {
    if (document.getElementById('orders-container')) {
        window.kitchenManager = new KitchenManager();
    }
});