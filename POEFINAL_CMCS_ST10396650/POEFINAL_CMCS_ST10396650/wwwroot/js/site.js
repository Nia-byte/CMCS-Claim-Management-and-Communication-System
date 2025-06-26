// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const policyVerification = {
    init: function () {
        this.setupPolicyChecks();
    },

    setupPolicyChecks: function () {
        document.querySelectorAll('tr').forEach(row => {
            if (!row.hasAttribute('data-claim-id')) return;

            const claimId = row.getAttribute('data-claim-id');
            const hoursWorked = parseFloat(row.querySelector('[data-hours]').textContent);
            const hourlyRate = parseFloat(row.querySelector('[data-rate]').textContent);
            const total = parseFloat(row.querySelector('[data-amount]').textContent);

            const policyRow = row.nextElementSibling;
            if (!policyRow.classList.contains('policy-verification-row')) return;

            const policyContainer = policyRow.querySelector('.policy-checks');
            this.renderPolicyChecks(policyContainer, {
                hoursWorked,
                hourlyRate,
                total
            });
        });
    },

    renderPolicyChecks: function (container, claim) {
        const policies = [
            {
                name: 'Weekly Hours',
                check: () => claim.hoursWorked <= 48,
                description: 'Max 48 hours/week'
            },
            {
                name: 'Claim Amount',
                check: () => claim.total <= 50000,
                description: 'Max R50,000'
            },
            {
                name: 'Hourly Rate',
                check: () => claim.hourlyRate >= 50 && claim.hourlyRate <= 300,
                description: 'R50 - R300/hour'
            }
        ];

        const html = policies.map(policy => {
            const isValid = policy.check();
            return `
                <div class="col-md-4">
                    <div class="policy-check-card ${isValid ? 'valid' : 'invalid'}">
                        <div class="policy-icon">
                            ${isValid ?
                    '<i class="fas fa-check-circle text-success"></i>' :
                    '<i class="fas fa-times-circle text-danger"></i>'
                }
                        </div>
                        <div class="policy-details">
                            <h6>${policy.name}</h6>
                            <small>${policy.description}</small>
                        </div>
                    </div>
                </div>
            `;
        }).join('');

        container.innerHTML = html;

        // Update approve button state
        const allPoliciesValid = policies.every(p => p.check());
        const row = container.closest('tr').previousElementSibling;
        const approveButton = row.querySelector('.btn-success');
        if (approveButton) {
            approveButton.disabled = !allPoliciesValid;
            if (!allPoliciesValid) {
                approveButton.title = 'Cannot approve - policy violations detected';
            }
        }
    }
};

// Add this to your document ready function
document.addEventListener('DOMContentLoaded', function () {
    policyVerification.init();
});
