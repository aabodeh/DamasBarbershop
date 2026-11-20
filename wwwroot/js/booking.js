(function () {
	var form = document.getElementById('damas-booking-form');
	if (!form) return;

	var steps = Array.prototype.slice.call(form.querySelectorAll('.booking-step'));
	var indicators = Array.prototype.slice.call(document.querySelectorAll('[data-step-indicator]'));
	var current = 1;
	var state = { service: '', barber: '', date: '', time: '' };

	function showStep(n) {
		steps.forEach(function (s) {
			s.classList.toggle('active', parseInt(s.getAttribute('data-step'), 10) === n);
		});
		indicators.forEach(function (d) {
			var i = parseInt(d.getAttribute('data-step-indicator'), 10);
			d.classList.toggle('active', i === n);
			d.classList.toggle('done', i < n);
		});
		current = n;
		if (n === 4) buildSummary();
	}

	form.querySelectorAll('[data-next]').forEach(function (btn) {
		btn.addEventListener('click', function () { showStep(current + 1); });
	});
	form.querySelectorAll('[data-prev]').forEach(function (btn) {
		btn.addEventListener('click', function () { showStep(current - 1); });
	});

	function selectCard(selector, field, hiddenId) {
		form.querySelectorAll(selector).forEach(function (card) {
			card.addEventListener('click', function () {
				form.querySelectorAll(selector).forEach(function (c) { c.classList.remove('selected'); });
				card.classList.add('selected');
				var value = card.getAttribute('data-select-' + field);
				state[field] = value;
				var hidden = document.getElementById(hiddenId);
				if (hidden) hidden.value = value;
				var nextBtn = card.closest('.booking-step').querySelector('[data-next]');
				if (nextBtn) nextBtn.disabled = false;
			});
		});
	}
	selectCard('[data-select-service]', 'service', 'field-service');
	selectCard('[data-select-barber]', 'barber', 'field-barber');
	selectCard('[data-select-time]', 'time', 'field-time');

	var dateInput = document.getElementById('date-input');
	if (dateInput) {
		var today = new Date().toISOString().split('T')[0];
		dateInput.setAttribute('min', today);
		dateInput.addEventListener('change', function () {
			state.date = dateInput.value;
			var hidden = document.getElementById('field-date');
			if (hidden) hidden.value = dateInput.value;
			checkStep3();
		});
	}
	function checkStep3() {
		var nextBtn = document.querySelector('.booking-step[data-step="3"] [data-next]');
		if (nextBtn) nextBtn.disabled = !(state.date && state.time);
	}
	form.querySelectorAll('[data-select-time]').forEach(function (slot) {
		slot.addEventListener('click', checkStep3);
	});

	function formatDate(iso) {
		if (!iso) return '';
		var d = new Date(iso + 'T00:00:00');
		return d.toLocaleDateString(undefined, { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' });
	}

	function buildSummary() {
		var box = document.getElementById('summary-box');
		if (!box) return;
		box.innerHTML =
			'<div class="summary-row"><span>Service</span><span>' + (state.service || '—') + '</span></div>' +
			'<div class="summary-row"><span>Barber</span><span>' + (state.barber || '—') + '</span></div>' +
			'<div class="summary-row"><span>Date</span><span>' + (formatDate(state.date) || '—') + '</span></div>' +
			'<div class="summary-row"><span>Time</span><span>' + (state.time || '—') + '</span></div>';
	}
})();
