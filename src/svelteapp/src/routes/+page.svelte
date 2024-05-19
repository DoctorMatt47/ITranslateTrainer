<script lang="ts">
  import { onMount } from "svelte";
  import { goto } from "$app/navigation";

  const { children } = $props();

  onMount(async () => {
    await goto("/menu");
  });
</script>

{@render children()}
