<script lang="ts">
  import { goto } from "$app/navigation";

  const { children } = $props();

  $effect.pre(() => {
    goto("/menu");
  });
</script>

{@render children()}
