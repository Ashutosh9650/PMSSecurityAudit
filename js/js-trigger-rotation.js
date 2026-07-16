const btn = document.querySelector(".js-trigger-rotation");



document.addEventListener("click", () => {
  const container = document.querySelector(".image-container");

  // Probably select current image here:
  const image = container.querySelector(".image");
  const width = image.offsetWidth;
  const height = image.offsetHeight;
  // this should be in data attributes or extracted from deg
  const currentRotateCycle = parseInt(
    getComputedStyle(image).getPropertyValue("--current-rotate-cycle")
  );

  if (currentRotateCycle % 2 === 0) {
    image.style.setProperty("--current-scale", "scale(" + height / width + ")");
  } else {
    image.style.setProperty("--current-scale", "scale(1)");
  }

  if (currentRotateCycle === 3) {
    image.style.setProperty("--current-rotate-cycle", 0);
    image.style.setProperty("--current-rotate", "rotate(0deg)");
  } else {
    const newRotateCycle = currentRotateCycle + 1;
    image.style.setProperty("--current-rotate-cycle", newRotateCycle);
    image.style.setProperty(
      "--current-rotate",
      "rotate(" + newRotateCycle * 90 + "deg)"
    );
  }
});
