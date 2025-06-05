// Masa düzeni yönetimi için JavaScript
class TableLayoutManager {
    constructor() {
        this.selectedTableId = null;
        this.initializeDragAndDrop();
        this.initializeEventListeners();
    }

    initializeDragAndDrop() {
        const tables = document.querySelectorAll('.table-item');
        const layout = document.getElementById('restaurant-layout');

        if (!layout) {
            console.error('restaurant-layout elementi bulunamadı!');
            return;
        }

        tables.forEach(table => {
            table.addEventListener('dragstart', (e) => {
                e.dataTransfer.setData('text/plain', table.dataset.tableId);
                table.style.opacity = '0.5';
                console.log('Drag başladı:', table.dataset.tableId);
            });

            table.addEventListener('dragend', (e) => {
                table.style.opacity = '1';
                console.log('Drag bitti');
            });

            table.addEventListener('click', (e) => {
                e.preventDefault();
                this.selectTable(table.dataset.tableId);
                console.log('Masa seçildi:', table.dataset.tableId);
            });
        });

        layout.addEventListener('dragover', (e) => {
            e.preventDefault();
        });

        layout.addEventListener('drop', (e) => {
            e.preventDefault();
            const tableId = e.dataTransfer.getData('text/plain');
            const rect = layout.getBoundingClientRect();
            const x = Math.max(0, e.clientX - rect.left - 40); // Center the table
            const y = Math.max(0, e.clientY - rect.top - 40);

            console.log(`Drop: tableId=${tableId}, x=${x}, y=${y}`);
            this.updateTablePosition(tableId, x, y);
        });
    }

    selectTable(tableId) {
        // Remove previous selection
        document.querySelectorAll('.table-item').forEach(item => {
            item.classList.remove('selected');
        });

        // Select current table
        const tableElement = document.querySelector(`[data-table-id="${tableId}"]`);
        if (tableElement) {
            tableElement.classList.add('selected');
        }

        this.selectedTableId = tableId;
        this.showTableDetails(tableId);
    }

    showTableDetails(tableId) {
        const detailsElement = document.getElementById('table-details');
        const noSelectionElement = document.getElementById('no-selection');

        if (detailsElement) {
            detailsElement.classList.remove('d-none');
        }
        if (noSelectionElement) {
            noSelectionElement.classList.add('d-none');
        }

        // Update table name
        const tableNameElement = document.getElementById('selected-table-name');
        if (tableNameElement) {
            tableNameElement.textContent = `Masa ${tableId}`;
        }
    }

    async updateTablePosition(tableId, x, y) {
        console.log(`updateTablePosition çağrıldı: tableId=${tableId}, x=${x}, y=${y}`);

        try {
            // Anti-forgery token'ı al
            const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

            const response = await fetch('/Admin/UpdateTablePosition', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token || ''
                },
                body: JSON.stringify({
                    tableId: parseInt(tableId),
                    x: Math.round(x),
                    y: Math.round(y)
                })
            });

            console.log('Response status:', response.status);

            if (response.ok) {
                const result = await response.json();
                console.log('Response data:', result);

                if (result.success) {
                    const tableElement = document.querySelector(`[data-table-id="${tableId}"]`);
                    if (tableElement) {
                        tableElement.style.left = `${Math.round(x)}px`;
                        tableElement.style.top = `${Math.round(y)}px`;
                    }

                    this.showToast('Masa konumu güncellendi!', 'success');
                } else {
                    this.showToast(result.message || 'Konum güncellenirken hata oluştu!', 'error');
                }
            } else {
                const errorText = await response.text();
                console.error('Server error:', errorText);
                this.showToast('Server hatası!', 'error');
            }
        } catch (error) {
            console.error('Position update error:', error);
            this.showToast('Konum güncellenirken hata oluştu!', 'error');
        }
    }

    initializeEventListeners() {
        // Assign server button
        const assignButton = document.getElementById('assign-server-btn');
        if (assignButton) {
            assignButton.addEventListener('click', async () => {
                if (!this.selectedTableId) {
                    this.showToast('Lütfen önce bir masa seçin!', 'warning');
                    return;
                }

                const serverId = document.getElementById('server-select')?.value;
                if (!serverId) {
                    this.showToast('Lütfen bir garson seçin!', 'warning');
                    return;
                }

                await this.assignServer(this.selectedTableId, serverId);
            });
        }

        // Update status button
        const statusButton = document.getElementById('update-status-btn');
        if (statusButton) {
            statusButton.addEventListener('click', async () => {
                if (!this.selectedTableId) {
                    this.showToast('Lütfen önce bir masa seçin!', 'warning');
                    return;
                }

                const status = document.getElementById('status-select')?.value;
                if (status) {
                    await this.updateTableStatus(this.selectedTableId, status);
                }
            });
        }
    }

    async assignServer(tableId, serverId) {
        console.log(`assignServer çağrıldı: tableId=${tableId}, serverId=${serverId}`);

        try {
            // Anti-forgery token'ı al
            const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

            const formData = new FormData();
            formData.append('tableId', tableId);
            formData.append('serverId', serverId);
            if (token) {
                formData.append('__RequestVerificationToken', token);
            }

            const response = await fetch('/Admin/AssignServer', {
                method: 'POST',
                body: formData
            });

            if (response.ok) {
                this.showToast('Garson ataması başarılı!', 'success');
                // Sayfayı yenile
                setTimeout(() => window.location.reload(), 1000);
            } else {
                this.showToast('Garson ataması başarısız!', 'error');
            }
        } catch (error) {
            console.error('Server assignment error:', error);
            this.showToast('Garson ataması başarısız!', 'error');
        }
    }

    async updateTableStatus(tableId, status) {
        console.log(`updateTableStatus çağrıldı: tableId=${tableId}, status=${status}`);

        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

            const response = await fetch('/Admin/UpdateTableStatus', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token || ''
                },
                body: JSON.stringify({
                    tableId: parseInt(tableId),
                    status: parseInt(status)
                })
            });

            if (response.ok) {
                const result = await response.json();
                console.log('Status update result:', result);

                if (result.success) {
                    this.showToast('Masa durumu güncellendi!', 'success');
                    // Sayfayı yenile
                    setTimeout(() => window.location.reload(), 1000);
                } else {
                    this.showToast(result.message || 'Durum güncellenirken hata oluştu!', 'error');
                }
            }
        } catch (error) {
            console.error('Status update error:', error);
            this.showToast('Durum güncellenirken hata oluştu!', 'error');
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

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    console.log('DOM yüklendi, table layout manager başlatılıyor...');

    if (document.getElementById('restaurant-layout')) {
        console.log('restaurant-layout bulundu, manager oluşturuluyor...');
        window.tableLayoutManager = new TableLayoutManager();
    } else {
        console.log('restaurant-layout bulunamadı!');
    }
});