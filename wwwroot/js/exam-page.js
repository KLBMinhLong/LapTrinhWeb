
      // Simple countdown timer
      document.addEventListener("DOMContentLoaded", function () {
        // Set the timer for 45 minutes (in seconds)
        let timeLeft = 45 * 60;

        function updateTimer() {
          const minutes = Math.floor(timeLeft / 60);
          const seconds = timeLeft % 60;

          // Format the time as MM:SS
          const formattedTime = `${minutes
            .toString()
            .padStart(2, "0")}:${seconds.toString().padStart(2, "0")}`;
          document.getElementById("countdown").textContent = formattedTime;

          // Change color when less than 5 minutes remaining
          if (timeLeft <= 300) {
            document.getElementById(
              "countdown"
            ).parentElement.style.backgroundColor = "#dc3545";
          }

          if (timeLeft > 0) {
            timeLeft--;
          } else {
            // Time's up - could trigger auto-submit here
            clearInterval(timerInterval);
            alert("Hết thời gian làm bài!");
          }
        }

        // Update the timer every second
        const timerInterval = setInterval(updateTimer, 1000);

        // Initial update
        updateTimer();
      });
    