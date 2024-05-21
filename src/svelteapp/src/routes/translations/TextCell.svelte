<script lang="ts">
  import { TextService, type TextStateItem } from "$lib/texts/text-service";

  const { text }: { text: TextStateItem } = $props();

  let newText = $state(text.value)

  const textService = new TextService();

  function updateText() {
    textService.updateText(text, newText)
  }
</script>

<input type="text" bind:value={newText} onfocusout={updateText}/>
