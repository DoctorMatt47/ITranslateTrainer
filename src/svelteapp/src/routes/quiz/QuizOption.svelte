<script lang="ts">
  import type { OptionApiResponse } from "$lib/services/test-service";

  export let option: OptionApiResponse;

  function buttonStyle(isChosen?: boolean, isCorrect?: boolean) {
    const border = isChosen ? "soft" : "ghost";
    const color = isCorrect ? "success" : isChosen ? "error" : "surface";
    return `${border}-${color}`;
  }
</script>

<button class="btn variant-{buttonStyle(option.isChosen, option.isCorrect)}" on:click type="button">
  {option.text}
</button>
