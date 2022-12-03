const curtirLabel = document.querySelector('#number')
const curtirButton = document.querySelector('#like')
const heart = document.querySelector('.coracao')

let bool = true

curtirButton.addEventListener('click', handleClick)
function handleClick() {
  bool = !bool


  if (bool === false) {
    heart.style.color = 'red'

    let i = 1
    return curtirLabel.innerHTML = i
  } else {
    heart.style.color = 'black'

    let i = 0
    return curtirLabel.innerHTML = i
  }
}