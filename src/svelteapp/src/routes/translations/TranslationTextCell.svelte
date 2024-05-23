<script lang="ts">
  import { TextService, type TextStateItem } from "$lib/texts/text-service";
  import AppTextInput from "$lib/common/components/AppTextInput.svelte";

  const { text }: { text: TextStateItem } = $props();

  let newText = $state(text.value);
  let isUpdating = $state(false);

  const textService = new TextService();

  function onfocusout() {
    if (text.value !== newText) {
      textService.updateText(text, newText);
    }

    isUpdating = false;
  }

  function onkeydown(event: KeyboardEvent) {
    if (event.key === "Enter") {
      onfocusout();
    }
  }

  function onclick() {
    isUpdating = true;
  }
</script>

{#if isUpdating}
  <AppTextInput focus={isUpdating} bind:value={newText} {onfocusout} {onkeydown} type="text" />
{:else}
  <button type="button" {onclick} class="py-1.5 w-1/2 h-full">{text.value}</button>
{/if}
