<script lang="ts">
  import type { OptionStateItem } from "$lib/tests/test-service.svelte";
  import type { MouseEventHandler } from "svelte/elements";

  const { option, onclick }: {
    option: OptionStateItem
    onclick: MouseEventHandler<HTMLButtonElement>
  } = $props();

  function buttonStyle(isSelected?: boolean, isCorrect?: boolean) {
    const border = isSelected ? "soft" : "ghost";
    const color = isCorrect ? "success" : isSelected ? "error" : "surface";
    return `${border}-${color}`;
  }
</script>

<button class="btn variant-{buttonStyle(option.isSelected, option.isCorrect)}" {onclick} type="button">
  {option.text}
</button>
